using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MemoryCacheLearn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace MemoryCacheLearn.Controllers
{
    public class HomeController : Controller
    {

        private readonly IMemoryCache _memoryCache;
        public HomeController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public IActionResult CacheTryGetValueSet()
        {
            DateTime cacheEntity;
            if (!_memoryCache.TryGetValue(CacheKeys.Entry, out cacheEntity))
            {
                //如果没有缓存 就获取当前时间，并加入缓存
                cacheEntity = DateTime.Now;
                var cacheOption = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(3));  //设置滑动缓存时间 3s 

                _memoryCache.Set(CacheKeys.Entry, cacheEntity, cacheOption);                                //加入缓存
            }
            return View("Cache", cacheEntity);
        }


        public IActionResult CacheGetOrCreate()
        {
            var cache = _memoryCache.GetOrCreate(CacheKeys.Entry, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(3);
                return DateTime.Now;
            });
            return View("Cache", cache);
        }

        public async Task<IActionResult> CacheGetOrCreateAsync()
        {
            var cache = await _memoryCache.GetOrCreateAsync(CacheKeys.Entry, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(3);
                return Task.FromResult(DateTime.Now);

            });
            return View("Cache", cache);
        }


        public IActionResult CacheGet()
        {
            var cacheEntry = _memoryCache.Get<DateTime?>(CacheKeys.Entry);
            return View("Cache", cacheEntry);
        }

        /// <summary>
        /// 设置绝对到期时间。 这是可以缓存条目的最长时间，可防止项持续续订滑动过期时变得太陈旧。
        /// 设置滑动过期时间。 访问此缓存的项的请求将重置滑动到期时钟。
        /// 将缓存优先级设置为CacheItemPriority.NeverRemove。
        /// 集PostEvictionDelegate后从缓存逐出项，将调用的。 回调是在从缓存中移除的项的代码与不同的线程上运行。
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateCallbackEntry()
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                // Pin to cache.
                .SetPriority(CacheItemPriority.NeverRemove)                                        //设置策略 从不移除
                // Add eviction callback
                .RegisterPostEvictionCallback(callback: EvictionCallback, state: this);            //设置手动被移除后，回调方法 EvictionCallback

            _memoryCache.Set(CacheKeys.CallbackEntry, DateTime.Now, cacheEntryOptions);            //设置 当前时间

            return RedirectToAction("GetCallbackEntry");
        }
        public IActionResult GetCallbackEntry()
        {
            return View("Callback", new CallbackViewModel
            {
                CachedTime = _memoryCache.Get<DateTime?>(CacheKeys.CallbackEntry),
                Message = _memoryCache.Get<string>(CacheKeys.CallbackMessage)
            });
        }

        public IActionResult RemoveCallbackEntry()
        {
            _memoryCache.Remove(CacheKeys.CallbackEntry);
            return RedirectToAction("GetCallbackEntry");
        }

        /// <summary>
        /// 缓存被移除时，回调函数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="reason"></param>
        /// <param name="state"></param>
        private static void EvictionCallback(object key, object value,
            EvictionReason reason, object state)
        {
            var message = $"Entry was evicted. Reason: {reason}.";
            ((HomeController)state)._memoryCache.Set(CacheKeys.CallbackMessage, message);
        }



        #region 缓存依赖项

        public IActionResult CreateDependentEntries()
        {
            var cts = new CancellationTokenSource();
            _memoryCache.Set(CacheKeys.DependentCTS, cts);

            using (var entry = _memoryCache.CreateEntry(CacheKeys.Parent))
            {
                // expire this entry if the dependant entry expires.
                entry.Value = DateTime.Now;
                entry.RegisterPostEvictionCallback(DependentEvictionCallback, this);

                _memoryCache.Set(CacheKeys.Child,
                    DateTime.Now,
                    new CancellationChangeToken(cts.Token));
            }

            return RedirectToAction("GetDependentEntries");
        }

        public IActionResult GetDependentEntries()
        {
            return View("Dependent", new DependentViewModel
            {
                ParentCachedTime = _memoryCache.Get<DateTime?>(CacheKeys.Parent),
                ChildCachedTime = _memoryCache.Get<DateTime?>(CacheKeys.Child),
                Message = _memoryCache.Get<string>(CacheKeys.DependentMessage)
            });
        }

        public IActionResult RemoveChildEntry()
        {
            _memoryCache.Get<CancellationTokenSource>(CacheKeys.DependentCTS).Cancel();
            return RedirectToAction("GetDependentEntries");
        }

        private static void DependentEvictionCallback(object key, object value,
            EvictionReason reason, object state)
        {
            var message = $"Parent entry was evicted. Reason: {reason}.";
            ((HomeController)state)._memoryCache.Set(CacheKeys.DependentMessage, message);
        }

        #endregion



    }
}
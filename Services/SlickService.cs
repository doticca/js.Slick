using System;
using System.Collections.Generic;
using System.Linq;
using Orchard;
using Orchard.Caching;
using Orchard.Environment.Extensions;
using Orchard.MediaLibrary.Services;
using js.Slick.Models;

namespace js.Slick.Services
{
    public interface ISlickService : IDependency
    {
        bool GetAutoEnable();
        bool GetAutoEnableAdmin();
    }

    public class SlickService : ISlickService
    {
        private readonly IWorkContextAccessor _wca;
        private readonly ICacheManager _cacheManager;
        private readonly ISignals _signals;
        private readonly IMediaLibraryService _mediaService;

        private const string ScriptsFolder = "scripts";

        public SlickService(IWorkContextAccessor wca, ICacheManager cacheManager, ISignals signals, IMediaLibraryService mediaService)
        {
            _wca = wca;
            _cacheManager = cacheManager;
            _signals = signals;
            _mediaService = mediaService;
        }

        public bool GetAutoEnable()
        {
            return _cacheManager.Get(
                "js.Slick.AutoEnable",
                ctx =>
                {
                    ctx.Monitor(_signals.When("js.Slick.Changed"));
                    WorkContext workContext = _wca.GetContext();
                    var slickSettings =
                        (SlickSettingsPart)workContext
                                                  .CurrentSite
                                                  .ContentItem
                                                  .Get(typeof(SlickSettingsPart));
                    return slickSettings.AutoEnable;
                });
        }

        public bool GetAutoEnableAdmin()
        {
            return _cacheManager.Get(
                "js.Slick.AutoEnableAdmin",
                ctx =>
                {
                    ctx.Monitor(_signals.When("js.Slick.Changed"));
                    WorkContext workContext = _wca.GetContext();
                    var slickSettings =
                        (SlickSettingsPart)workContext
                                                  .CurrentSite
                                                  .ContentItem
                                                  .Get(typeof(SlickSettingsPart));
                    return slickSettings.AutoEnableAdmin;
                });
        }

    }
}
using System;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.TestWidget.Models;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.TestWidget.Components
{
    [ViewComponent(Name = "WidgetsTestWidget")]
    public class WidgetsTestWidgetViewComponent : NopViewComponent
    {
        private readonly IStoreContext _storeContext;
        private readonly IStaticCacheManager _cacheManager;
        private readonly ISettingService _settingService;
        private readonly IPictureService _pictureService;
        private readonly IWebHelper _webHelper;

        public WidgetsTestWidgetViewComponent(IStoreContext storeContext, 
            IStaticCacheManager cacheManager, 
            ISettingService settingService, 
            IPictureService pictureService,
            IWebHelper webHelper)
        {
            _storeContext = storeContext;
            _cacheManager = cacheManager;
            _settingService = settingService;
            _pictureService = pictureService;
            _webHelper = webHelper;
        }

        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            var TestWidgetSettings = _settingService.LoadSetting<TestWidgetSettings>(_storeContext.CurrentStore.Id);

            var model = new PublicInfoModel
            {
                Text = TestWidgetSettings.Text
            };

            return View("~/Plugins/Widgets.TestWidget/Views/PublicInfo.cshtml", model);
        }

    }
}

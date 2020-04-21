using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.TestWidget.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.TestWidget.Controllers
{
    [Area(AreaNames.Admin)]
    public class WidgetsTestWidgetController : BasePluginController
    {
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;

        public WidgetsTestWidgetController(ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService,
            ISettingService settingService,
            IStoreContext storeContext)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _settingService = settingService;
            _storeContext = storeContext;
        }

        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var TestWidgetSettings = _settingService.LoadSetting<TestWidgetSettings>(storeScope);
            var model = new ConfigurationModel
            {
                Text = TestWidgetSettings.Text,
                ActiveStoreScopeConfiguration = storeScope
            };

            return View("~/Plugins/Widgets.TestWidget/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var TestWidgetSettings = _settingService.LoadSetting<TestWidgetSettings>(storeScope);

            TestWidgetSettings.Text = model.Text;
            _settingService.SaveSettingOverridablePerStore(TestWidgetSettings, x => x.Text, model.Text_OverrideForStore, storeScope, false);
            //now clear settings cache
            _settingService.ClearCache();
            
            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }
    }
}
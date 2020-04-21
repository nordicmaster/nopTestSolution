using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.TestWidget.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }

        public bool Text_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.TestWidget.Text")]
        public string Text { get; set; }

    }
}
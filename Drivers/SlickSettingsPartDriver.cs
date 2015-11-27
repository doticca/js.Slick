using js.Slick.Models;
using js.Slick.Services;
using Orchard.Caching;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using js.Slick.ViewModels;

namespace js.Slick.Drivers
{
    public class SlickSettingsPartDriver : ContentPartDriver<SlickSettingsPart> 
    {
        private readonly ISignals _signals;
        private readonly ISlickService _slickService;

        public SlickSettingsPartDriver(ISignals signals, ISlickService slickService)
        {
            _signals = signals;
            _slickService = slickService;
        }

        protected override string Prefix { get { return "SlickSettings"; } }

        protected override DriverResult Editor(SlickSettingsPart part, dynamic shapeHelper)
        {

            return ContentShape("Parts_Slick_SlickSettings",
                               () => shapeHelper.EditorTemplate(
                                   TemplateName: "Parts/Slick.SlickSettings",
                                   Model: new SlickSettingsViewModel
                                   {
                                       AutoEnable = part.AutoEnable,
                                       AutoEnableAdmin = part.AutoEnableAdmin,
                                   },
                                   Prefix: Prefix)).OnGroup("Slick");
        }

        protected override DriverResult Editor(SlickSettingsPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part.Record, Prefix, null, null);
            _signals.Trigger("js.Slick.Changed");
            return Editor(part, shapeHelper);
        }

        protected override void Exporting(SlickSettingsPart part, ExportContentContext context)
        {
            var element = context.Element(part.PartDefinition.Name);

            element.SetAttributeValue("AutoEnable", part.AutoEnable);
            element.SetAttributeValue("AutoEnableAdmin", part.AutoEnableAdmin);
        }

        protected override void Importing(SlickSettingsPart part, ImportContentContext context)
        {
            var partName = part.PartDefinition.Name;

            part.Record.AutoEnable = GetAttribute<bool>(context, partName, "AutoEnable");
            part.Record.AutoEnableAdmin = GetAttribute<bool>(context, partName, "AutoEnableAdmin");
        }

        private TV GetAttribute<TV>(ImportContentContext context, string partName, string elementName)
        {
            string value = context.Attribute(partName, elementName);
            if (value != null)
            {
                return (TV)Convert.ChangeType(value, typeof(TV));
            }
            return default(TV);
        }
    }
}
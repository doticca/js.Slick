using js.Slick.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace js.Slick.Handlers
{
    public class SlickSettingsPartHandler: ContentHandler {
        public SlickSettingsPartHandler(IRepository<SlickSettingsPartRecord> repository)
        {
            T = NullLocalizer.Instance;
            Filters.Add(StorageFilter.For(repository));
            Filters.Add(new ActivatingFilter<SlickSettingsPart>("Site"));

            OnInitializing<SlickSettingsPart>((context, part) =>
            {
                part.AutoEnable = true;
                part.AutoEnableAdmin = true;
            });
        }

        public Localizer T { get; set; }

        protected override void GetItemMetadata(GetContentItemMetadataContext context) {
            if (context.ContentItem.ContentType != "Site")
                return;
            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("Slick")));
        }
    }
}
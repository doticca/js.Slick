using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace js.Slick.Models
{
    public class SlickSettingsPartRecord: ContentPartRecord
    {
        public virtual bool AutoEnable { get; set; }
        public virtual bool AutoEnableAdmin { get; set; }

        public SlickSettingsPartRecord()
        {
            AutoEnable = true;
            AutoEnableAdmin = false;
        }
    }
}
using ProjectArrow.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace ProjectArrow.Entity
{
    public class Barrer
    {
        public string Name { get; set; }
        public int EquipId { get; set; }
        public string TagAddress { get; set; }
        public int EmptyValue { get; set; }

        public async void BarrelExecute(UnifiedAutomation.UaBase.DataValue value)
        {
            if (Convert.ToInt32(value.Value) == EmptyValue)
            {
                await new TaskHelper().BarrelLeave(EquipId);
            }
        }
    }
}
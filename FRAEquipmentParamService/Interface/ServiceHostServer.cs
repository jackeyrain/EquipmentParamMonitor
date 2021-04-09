using FRAEquipmentParamService.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace FRAEquipmentParamService.Interface
{
    public class ServiceHostServer
    {
        private ServiceHost host = null;
        public static List<StationEntity> StationEntities;

        public ServiceHostServer(List<StationEntity> stationEntities)
        {
            StationEntities = stationEntities;
        }

        public void Start()
        {
            Uri address = new Uri($"http://{ConfigurationManager.AppSettings["ServicePort"]}/Guidline");
            LogHelper.Log.LogInfo(string.Format("WCF Start on: {0}", address));
            host = new ServiceHost(typeof(GuidLine), address);
            ServiceMetadataBehavior behaviour = new ServiceMetadataBehavior();
            behaviour.HttpGetEnabled = true;
            host.Description.Behaviors.Add(behaviour);

            WSHttpBinding binding = new WSHttpBinding { Security = { Mode = SecurityMode.None } };
            host.AddServiceEndpoint(typeof(IGuidLine), binding, "Guid Line interface Service");

            host.Open();
        }

        public void Stop()
        {
            host?.Close();
        }
    }
}

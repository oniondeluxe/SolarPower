using OnionDlx.SolPwr.Data;
using OnionDlx.SolPwr.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.Services
{
    public interface IMeteoLookupService
    {
        Task<IEnumerable<MeteoData>> GetMeteoDataAsync(GeoCoordinate geoCoordinate, TimeResolution resol, TimeSpanCode code, int timeSpan);
    }


    public interface IMeteoLookupServiceCallback
    {
        event EventHandler<IMeteoLookupService> ServicePushUpdate;

        void OnRequestEndpoint(Func<IMeteoLookupService> getService);

        IMeteoLookupService GetEndpoint();

        void InvokePush(IMeteoLookupService service);

        void Teardown();
    }
}

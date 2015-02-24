using arkitektum.kommit.ebyggesak.Models;
using Arkitektum.kommit.ebyggesak.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.OData.Query;

namespace arkitektum.kommit.ebyggesak.Controllers
{
    public class ByggesakController : ApiController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        [Route("api")]
        [HttpGet]
        public LinkListeType GetApi1()
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            
            List<LinkType> linker = new List<LinkType>();

            linker.Add(Set.addTempLink(baseUri, "api/byggesaker", Set._REL + "/byggesak", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addTempLink(baseUri, "api/vedtak", Set._REL + "/vedtak", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addTempLink(baseUri, "api/prosesser", Set._REL + "/prosess", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addTempLink(baseUri, "api/aktiviteter", Set._REL + "/aktivitet", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addTempLink(baseUri, "api/milepeler", Set._REL + "/milepel", "?$filter&$orderby&$top&$skip&$search"));

            LinkListeType liste = new LinkListeType();
            liste._links = linker.ToArray();
            return liste;
        }

        [Route("api/byggesaker")]
        [HttpGet]
        public IEnumerable<ByggesakType> GetByggesaker(ODataQueryOptions<ByggesakType> queryOptions)
        {
      
            queryOptions.Validate(_validationSettings);

            List<ByggesakType> testdata = new List<ByggesakType>();

            //TODO Håndtere filter... 

            //if (queryOptions.Filter != null)
            //{
            //    var q = queryOptions.Filter.FilterClause.Expression;
            //    if (queryOptions.Filter.RawValue.Contains("systemID"))
            //    {
            //        var mockarkiv = GetByggesak("fra filter eller ");
            //        //mockarkiv.beskrivelse = "passe filter";
            //        testdata.Add(GetByggesak(((Microsoft.Data.OData.Query.SemanticAst.ConstantNode)(((Microsoft.Data.OData.Query.SemanticAst.BinaryOperatorNode)(queryOptions.Filter.FilterClause.Expression)).Right)).Value.ToString()));
            //    }
            //}


            if (queryOptions.Top == null)
            {
                testdata.Add(GetByggesak("12345"));
                testdata.Add(GetByggesak("234"));
                testdata.Add(GetByggesak(Guid.NewGuid().ToString()));
                testdata.Add(GetByggesak(Guid.NewGuid().ToString()));
                testdata.Add(GetByggesak(Guid.NewGuid().ToString()));
                testdata.Add(GetByggesak(Guid.NewGuid().ToString()));
                testdata.Add(GetByggesak(Guid.NewGuid().ToString()));
                testdata.Add(GetByggesak(Guid.NewGuid().ToString()));
                testdata.Add(GetByggesak(Guid.NewGuid().ToString()));
                testdata.Add(GetByggesak(Guid.NewGuid().ToString()));
            }
            else if (queryOptions.Top != null)
            {
                while (testdata.Count < queryOptions.Top.Value)
                {
                    testdata.Add(GetByggesak(Guid.NewGuid().ToString()));
                }


            }


            return testdata.AsEnumerable();

        }

        [Route("api/byggesak/{id}")]
        [HttpGet]
        public ByggesakType GetByggesak(string id)
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            ByggesakType m = new ByggesakType();
            m.tittel = "Enebolig testgate 26";
            m.systemId = id;
            m.saksnummer = new SaksnummerType() { saksaar = "2014", sakssekvensnummer = "12345" };
            m.adresse = "testgate 26";
            m.bygningsnummer = "123456789";
            List<SaksnummerType> referanser = new List<SaksnummerType>();
            referanser.Add(new SaksnummerType() { saksaar = "2014", sakssekvensnummer = "123" });
            m.referanseAndreSaker = referanser.ToArray();


            List<LinkType> linker = new List<LinkType>();
            linker.Add(Set.addLink(baseUri, "api/byggesak/" + m.systemId, "self"));
            linker.Add(Set.addTempLink(baseUri, "api/byggesak/" + m.systemId + "/prosesser", Set._REL + "/prosess", "?$filter&$orderby&$top&$skip&$search"));

            m._links = linker.ToArray();
            if (m == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return m;
        }

        [Route("api/byggesak/{id}/prosesser")]
        [HttpGet]
        public IEnumerable<ProsessType> GetProsesser(string id, ODataQueryOptions<ProsessType> queryOptions)
        {

            queryOptions.Validate(_validationSettings);

            List<ProsessType> testdata = new List<ProsessType>();


            if (queryOptions.Top == null)
            {
                
                testdata.Add(GetProsess(Guid.NewGuid().ToString()));
                testdata.Add(GetProsess(Guid.NewGuid().ToString()));
                testdata.Add(GetProsess(Guid.NewGuid().ToString()));
                testdata.Add(GetProsess(Guid.NewGuid().ToString()));
                testdata.Add(GetProsess(Guid.NewGuid().ToString()));
                testdata.Add(GetProsess(Guid.NewGuid().ToString()));
                testdata.Add(GetProsess(Guid.NewGuid().ToString()));
                testdata.Add(GetProsess(Guid.NewGuid().ToString()));
            }
            else if (queryOptions.Top != null)
            {
                while (testdata.Count < queryOptions.Top.Value)
                {
                    testdata.Add(GetProsess(Guid.NewGuid().ToString()));
                }


            }


            return testdata.AsEnumerable();

        }

        [Route("api/prosesser")]
        [HttpGet]
        public IEnumerable<ProsessType> GetProsesser(ODataQueryOptions<ProsessType> queryOptions)
        {

            queryOptions.Validate(_validationSettings);

            List<ProsessType> testdata = new List<ProsessType>();


            if (queryOptions.Top == null)
            {
                testdata.Add(GetProsess("12345"));
                testdata.Add(GetProsess("234"));
                testdata.Add(GetProsess(Guid.NewGuid().ToString()));
                testdata.Add(GetProsess(Guid.NewGuid().ToString()));
                testdata.Add(GetProsess(Guid.NewGuid().ToString()));
                testdata.Add(GetProsess(Guid.NewGuid().ToString()));
                testdata.Add(GetProsess(Guid.NewGuid().ToString()));
                testdata.Add(GetProsess(Guid.NewGuid().ToString()));
                testdata.Add(GetProsess(Guid.NewGuid().ToString()));
                testdata.Add(GetProsess(Guid.NewGuid().ToString()));
            }
            else if (queryOptions.Top != null)
            {
                while (testdata.Count < queryOptions.Top.Value)
                {
                    testdata.Add(GetProsess(Guid.NewGuid().ToString()));
                }


            }


            return testdata.AsEnumerable();

        }

        [Route("api/prosess/{id}")]
        [HttpGet]
        public ProsessType GetProsess(string id)
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            ProsessType m = new ProsessType();
            m.kategori = new ProsesskategoriType() { kode = "ET", beskrivelse = "Behandle ett trinnssøknad" };
            m.systemId = id;
            m.antallUkerFrist = "3";
            m.medDispensasjon = false;
            m.medReguleringsendring = false;
            m.prosesskoe = "kø enkel";
            m.prosessteam = "Team 2";
            m.prosessansvarlig = "Saksbehandler 2";
           

            List<LinkType> linker = new List<LinkType>();
            linker.Add(Set.addLink(baseUri, "api/prosess/" + m.systemId, "self"));
            linker.Add(Set.addLink(baseUri, "api/byggesak/" + m.systemId, Set._REL + "/byggesak"));
            linker.Add(Set.addTempLink(baseUri, "api/prosess/" + m.systemId + "/milepeler", Set._REL + "/milepel", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addTempLink(baseUri, "api/prosess/" + m.systemId + "/vedtak", Set._REL + "/vedtak", "?$filter&$orderby&$top&$skip&$search"));
            linker.Add(Set.addTempLink(baseUri, "api/prosess/" + m.systemId + "/mangler", Set._REL + "/mangel", "?$filter&$orderby&$top&$skip&$search"));

            m._links = linker.ToArray();
            if (m == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return m;
        }
    }
}

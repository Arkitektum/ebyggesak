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
        public LinkListeType GetRootApi()
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
            linker.Add(Set.addTempLink(baseUri, "api/mangler", Set._REL + "/mangel", "?$filter&$orderby&$top&$skip&$search"));

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
        public IEnumerable<ProsessType> GetProsesserOnByggesak(string id, ODataQueryOptions<ProsessType> queryOptions)
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

        [Route("api/prosess/{id}/milepeler")]
        [HttpGet]
        public IEnumerable<MilepelType> GetMilepelerOnProsess(string id, ODataQueryOptions<MilepelType> queryOptions)
        {

            queryOptions.Validate(_validationSettings);

            List<MilepelType> testdata = new List<MilepelType>();


            if (queryOptions.Top == null)
            {

                testdata.Add(GetMilepel(Guid.NewGuid().ToString()));
                testdata.Add(GetMilepel(Guid.NewGuid().ToString()));
                testdata.Add(GetMilepel(Guid.NewGuid().ToString()));
                testdata.Add(GetMilepel(Guid.NewGuid().ToString()));
                
            }
            else if (queryOptions.Top != null)
            {
                while (testdata.Count < queryOptions.Top.Value)
                {
                    testdata.Add(GetMilepel(Guid.NewGuid().ToString()));
                }


            }


            return testdata.AsEnumerable();

        }

        [Route("api/milepel/{id}")]
        [HttpGet]
        public MilepelType GetMilepel(string id)
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            MilepelType m = new MilepelType();
            m.kategori = new MilepeltypeType() { kode = "MS", beskrivelse = "Mottatt søknad" };
            m.systemId = id;
            m.beskrivelse = "Søknad om enebolig";
            m.referanseJournalpost = "23456";
            m.referanseDokument = "23535";
            m.utfoert = DateTime.Now;
            m.utfoertSpecified = true;

            List<LinkType> linker = new List<LinkType>();
            linker.Add(Set.addLink(baseUri, "api/milepel/" + m.systemId, "self"));
            linker.Add(Set.addLink(baseUri, "api/prosess/" + m.systemId, Set._REL + "/prosess"));
            linker.Add(Set.addTempLink(baseUri, "api/milepel/" + m.systemId + "/aktiviteter", Set._REL + "/aktivitet", "?$filter&$orderby&$top&$skip&$search"));

            m._links = linker.ToArray();
            if (m == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return m;
        }

        [Route("api/prosess/{id}/vedtak")]
        [HttpGet]
        public IEnumerable<VedtakType> GetVedtakOnProsess(string id, ODataQueryOptions<VedtakType> queryOptions)
        {

            queryOptions.Validate(_validationSettings);

            List<VedtakType> testdata = new List<VedtakType>();


            if (queryOptions.Top == null)
            {

                testdata.Add(GetVedtak(Guid.NewGuid().ToString()));
                testdata.Add(GetVedtak(Guid.NewGuid().ToString()));
                

            }
            else if (queryOptions.Top != null)
            {
                while (testdata.Count < queryOptions.Top.Value)
                {
                    testdata.Add(GetVedtak(Guid.NewGuid().ToString()));
                }


            }


            return testdata.AsEnumerable();

        }

        [Route("api/vedtak/{id}")]
        [HttpGet]
        public VedtakType GetVedtak(string id)
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            VedtakType m = new VedtakType();
            m.status = new VedtakstypeType() { kode = "1", beskrivelse = "Godkjent" };
            
            m.beskrivelse = "Vedtak om oppføring av enebolig";
            m.referanseVedtaksdokument = "23535";
            m.referanseUnderlag = new string[] {"34544", "45434"};
            m.vedtaksdato = DateTime.Now;

            List<LinkType> linker = new List<LinkType>();
            linker.Add(Set.addLink(baseUri, "api/vedtak/" + id, "self"));
            linker.Add(Set.addLink(baseUri, "api/prosess/" + id, Set._REL + "/prosess"));
            

            m._links = linker.ToArray();
            if (m == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return m;
        }

        [Route("api/milepel/{id}/aktiviteter")]
        [HttpGet]
        public IEnumerable<MilepelType> GetAktiviteterOnMilepel(string id, ODataQueryOptions<MilepelType> queryOptions)
        {

            queryOptions.Validate(_validationSettings);

            List<MilepelType> testdata = new List<MilepelType>();


            if (queryOptions.Top == null)
            {

                testdata.Add(GetMilepel(Guid.NewGuid().ToString()));
                testdata.Add(GetMilepel(Guid.NewGuid().ToString()));


            }
            else if (queryOptions.Top != null)
            {
                while (testdata.Count < queryOptions.Top.Value)
                {
                    testdata.Add(GetMilepel(Guid.NewGuid().ToString()));
                }


            }


            return testdata.AsEnumerable();

        }

        [Route("api/aktivitet/{id}")]
        [HttpGet]
        public AktivitetType GetAktivitet(string id)
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            AktivitetType m = new AktivitetType();
            m.status = new AktivitetsstatusType() { kode = "UF", beskrivelse = "Utført" };
            m.typeSjekk = new SjekkpunkttypeType() { kode = "VT", beskrivelse = "Vurdert tiltaksklasse" };
            m.beskrivelse = "...";
           
            List<LinkType> linker = new List<LinkType>();
            linker.Add(Set.addLink(baseUri, "api/aktivitet/" + id, "self"));
            linker.Add(Set.addLink(baseUri, "api/milepel/" + id, Set._REL + "/milepel"));


            m._links = linker.ToArray();
            if (m == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return m;
        }

        [Route("api/prosess/{id}/mangler")]
        [HttpGet]
        public IEnumerable<MangelType> GetManglerOnProsess(string id, ODataQueryOptions<MangelType> queryOptions)
        {

            queryOptions.Validate(_validationSettings);

            List<MangelType> testdata = new List<MangelType>();


            if (queryOptions.Top == null)
            {

                testdata.Add(GetMangel(Guid.NewGuid().ToString()));
                testdata.Add(GetMangel(Guid.NewGuid().ToString()));


            }
            else if (queryOptions.Top != null)
            {
                while (testdata.Count < queryOptions.Top.Value)
                {
                    testdata.Add(GetMangel(Guid.NewGuid().ToString()));
                }


            }


            return testdata.AsEnumerable();

        }

        [Route("api/mangel/{id}")]
        [HttpGet]
        public MangelType GetMangel(string id)
        {
            var url = HttpContext.Current.Request.Url;
            var baseUri =
                new UriBuilder(
                    url.Scheme,
                    url.Host,
                    url.Port).Uri;

            MangelType m = new MangelType();
            m.status = "UF";// new AktivitetsstatusType() { kode = "UF", beskrivelse = "Utført" };
            m.type = new MangeltypeType() { kode = "KART", beskrivelse = "Mangler situasjonskart" };
            

            List<LinkType> linker = new List<LinkType>();
            linker.Add(Set.addLink(baseUri, "api/mangel/" + id, "self"));
            linker.Add(Set.addLink(baseUri, "api/prosess/" + id, Set._REL + "/prosess"));


            m._links = linker.ToArray();
            if (m == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return m;
        }
    }
}

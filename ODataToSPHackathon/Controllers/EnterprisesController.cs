using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using ODataToSPHackathon.Models;
using ODataToSPHackathon.Repositories;
using System.Web;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Microsoft.OData.Edm;
using Microsoft.OData.Core.UriParser;
using Microsoft.OData.Core.UriParser.Semantic;
using ODataToSPHackathon.Infrastructure;

namespace ODataToSPHackathon.Controllers
{
    
    public class EnterprisesController : ODataController
    {
        private PartnerDbContext db = new PartnerDbContext();
        private IEdmModel _model;

        /// <summary>
        /// Foi necessário esse Construtor para poder obter um Model, usado na busca das queries
        /// TODO: Estudar o Controller OData para encontrar melhor maneira de buscar isso, pois já foi iniciado no Startup da API
        /// </summary>
        public EnterprisesController()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<EnterprisesModel>("Enterprises");
            _model = builder.GetEdmModel();
        }
        
        
        /// <summary>
        /// Esse Controller está implementando a lógica para converter parametros OData para parametros de StoredProcedure.
        /// Não dá pra usar [EnableQuery], senão os parametros da query serão aplicados na SP e posteriormente no retorno dela.        
        /// </summary>
        /// <param name="queryOptions"></param>
        /// <returns></returns>
        // GET: odata/Enterprises        
        public IQueryable<EnterprisesModel> GetEnterprises(ODataQueryOptions queryOptions)
        {
            //TODO: O código abaixo deve ser posteriormente movido para um Filter do Controller, 
            // por ser genérico irá se repetir em cada GET do Controller.
            Uri fullUri = new Uri(ControllerContext.Request.RequestUri.PathAndQuery.Replace("odata/", ""), UriKind.Relative);
            ODataUriParser parser = new ODataUriParser(_model, fullUri);                //Poderia ser via ODataQueryOptionParser, mas achei mais complicado            

            FilterClause filterClause = parser.ParseFilter();                           // parse $filter
            OrderByClause orderByClause = parser.ParseOrderBy();                        // parse $orderby
            long? topClause = parser.ParseTop();                                        // parse $top
            long? skipClause = parser.ParseSkip();                                      // parse $skip
            bool count = parser.ParseCount() ?? false;                                  // parse $count

            SqlFilterBinder filterBinder = new SqlFilterBinder(filterClause);           //Binder para resolver o filtro; TODO: Melhorar para componente e UNIT TESTE
            SqlOrderByBinder orderByBinder = new SqlOrderByBinder(orderByClause);       //Binder para resolver o ordenação; TODO: Melhorar para componente e UNIT TESTE
            SqlPagingBinder pagingBinder = new SqlPagingBinder(topClause, skipClause);  //Binder para resolver o paginação; TODO: Melhorar para componente e UNIT TESTE

            //Agora é só aplicar os parametros encontrados na Stored Procedure
            return db.ProcPgdEnterprisesRetrieveAllMonitoredFromUserPartner(7, filterBinder.Resolve(), orderByBinder.Resolve(), pagingBinder.Resolve(), count).AsQueryable();
        }
       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
    }
}

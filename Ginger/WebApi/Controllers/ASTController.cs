﻿using GingerParser;
using GingerParser.CFG;
using GingerParser.DDG;
using GingerParser.Scope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [EnableCors(origins: "http://localhost:4200, http://gngr.io, http://www.gngr.io, http://noilly.github.io", headers: "*", methods: "*")]
    public class ASTController : ApiController
    {
        public IHttpActionResult Post([FromBody]string source)
        {
            if (source == null)
            {
                return BadRequest();
            }

            Parser parser = new Parser(source);
            parser.parse();
            //CFGVisitor cfgv = new CFGVisitor(parser.ast);
            ScopeVisitor sv = new ScopeVisitor(parser.ast);
            //DDGVisitor ddgv = new DDGVisitor(parser.ast);
            AstJsonVisitor jsonv = new AstJsonVisitor(parser.ast);

            return Ok(jsonv.graph);
        }
    }
}

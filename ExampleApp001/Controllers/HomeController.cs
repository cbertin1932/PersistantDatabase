using ExampleApp001.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExampleApp001.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string input = "")
        {
            if (input.Equals(""))
            {
                return View();
            }
            else
            {
                return Json(input);
            }
        }

        //easy way to build the javascript
        private string createJS(string result, string alias = "")
        {
            string js = string.Format("$('#result').text('{0}');", result);
            if (alias.Length > 0)
                js += string.Format("$('#alias').val('{0}');", alias);
            return js;
        }
        //The CRUD

        public JavaScriptResult Create(string name, string alias)
        {
            //retrieves the list
            List<AliasModel> aliases = (List<AliasModel>)Session["aliasList"];
            //if the list is null, were create it.
            if (aliases == null)
            {
                aliases = new List<AliasModel>();
                aliases.Add(new AliasModel() { Name = name, Alias = alias });
                Session["aliasList"] = aliases;
                return JavaScript(createJS("Name created."));
            }

            if (aliases.Exists(x => x.Alias.Equals(alias)))
            {
                return JavaScript(createJS("Alias already exists."));
            }
            aliases.Add(new AliasModel() { Name = name, Alias = alias });
            Session["aliasList"] = aliases;
            return JavaScript(createJS("Name created."));
        }

        public JavaScriptResult Retrieve(string name)
        {
            List<AliasModel> aliases = (List<AliasModel>)Session["aliasList"];

            if (aliases == null)
                return JavaScript(createJS("No entries with that name."));

            foreach (AliasModel a in aliases)
            {
                if(a.Name.Equals(name))
                    return JavaScript(createJS("Retrieved", a.Alias));
            }
            return JavaScript(createJS("No such name."));
        }

        public JavaScriptResult Update(string name, string alias)
        {
            List<AliasModel> aliases = (List<AliasModel>)Session["aliasList"];

            if (aliases == null)
                return JavaScript(createJS("No entries stored to update."));

            foreach (AliasModel a in aliases)
            {
                if (a.Name.Equals(name))
                {
                    a.Name = name;
                    a.Alias = alias;
                    Session["aliasList"] = aliases;
                    return JavaScript(createJS("Updated", a.Alias));
                }
            }
            return JavaScript(createJS("No such entry.\nTry creating it."));
        }

        public JavaScriptResult Delete(string name)
        {
            List<AliasModel> aliases = (List<AliasModel>)Session["aliasList"];

            if (aliases == null)
                return JavaScript(createJS("No entries stored to delete."));

            if (aliases.RemoveAll(x => x.Name == name)>0)
            {
                Session["aliasList"] = aliases;
                return JavaScript(createJS("Deleted"));
            }
            return JavaScript(createJS("Name doesn't exists."));
        }

    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GingerParser.Scope
{
    public class Scope
    {
        private Scope _parent;
        private Dictionary<Identifier, Declaration> locals;
        private List<Scope> children;

        public Scope parent
        {
            get { return _parent; }
        }

        public Scope()
        {
            initialise();
        }

        public Scope(Scope parent)
        {
            this._parent = parent;
            initialise();
        }

        public void add(Declaration declaration)
        {
            // an identifier cannot be declared more than once in the same scope
            if (locals.ContainsKey(declaration.identifier)) {
                throw new ScopeException(declaration.identifier.row, declaration.identifier.col, "An identifier cannot be used twice within the same scope.");
            }

            locals.Add(declaration.identifier, declaration);
        }

        public void add(Scope scope)
        {
            children.Add(scope);
        }

        public Declaration find(Identifier identifier)
        {
            if (locals.ContainsKey(identifier))
            {
                return locals[identifier];
            }
            else if (hasParent())
            {
                return _parent.find(identifier);
            }
            else
            {
                throw new ScopeException(identifier.row, identifier.col, "This identifier has not been declared.");
            }
        }

        public bool hasParent()
        {
            return _parent != null;
        }

        private void initialise()
        {
            children = new List<Scope>();
            locals = new Dictionary<Identifier, Declaration>();
        }
    }

    public class ScopeException : ParseException
    {
        public ScopeException(int row, int col, string reason) : base(row, col, reason, ExceptionLevel.ERROR)
        {

        }
    }
}
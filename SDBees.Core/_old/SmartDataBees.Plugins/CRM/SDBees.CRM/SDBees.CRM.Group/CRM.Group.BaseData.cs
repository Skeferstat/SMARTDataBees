// #StartHeader# ==============================================================
//
// This file is a part of the SMARTDataBees open source project.
//
// Copyright (C) 2007 by
//        G.E.M. Team Solutions GbR
//        CAD-Development
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//
// #EndHeader# ================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SDBees.DB;

namespace SDBees.CRM.Group
{
    public class CRMGroupBaseData : SDBees.Plugs.TemplateTreenode.TemplateTreenodeBaseData
    {
        #region Private Data Members

        internal static Table gTable = null;

        #endregion

        #region Public Properties
        public override string GetTableName
        {
            get { return "nameCRMGroup"; }
        }

        #endregion

        #region Constructor/Destructor

        /// <summary>
        /// Standard constructor
        /// </summary>
        public CRMGroupBaseData()
            :
            base("Kontaktgruppenname", "Gruppe", "Allgemein")
        {
            base.Table = gTable;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the Plugin this data is stored in
        /// </summary>
        /// <returns></returns>
        public override SDBees.Plugs.TemplateTreenode.TemplateTreenode GetPlugin()
        {
            return CRMGroup.Current;
        }

        #endregion

        #region Protected Methods
        /*
        /// <summary>
        /// Returns the table name the information is stored in
        /// </summary>
        /// <returns></returns>
        protected override string TableName()
        {
            return "nameCRMGroup";
        }
         * */

        #endregion
    }
}

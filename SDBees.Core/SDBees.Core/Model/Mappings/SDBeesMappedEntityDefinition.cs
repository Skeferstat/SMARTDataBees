﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace SDBees.Core.Model
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

    using System.Runtime.Serialization;

    /// <summary>
    /// Mapping eines Entities im Plugin
    /// </summary>
	[DataContract]
	public class SDBeesMappedEntityDefinition
	{
        public SDBeesMappedEntityDefinition()
        {
            EntityDefinitionId = new SDBeesEntityDefinitionId();
            m_pluginEntityInfo = new SDBeesExternalEntityInfo();
            m_propertyMappings = new HashSet<SDBeesMappedPropertyDefinition>();
            m_filter = new SDBeesEntityFilter();
            m_requiredSourceRelationships = new HashSet<SDBeesRelationshipDefinitionId>();
            m_requiredTargetRelationships = new HashSet<SDBeesRelationshipDefinitionId>();
        }

        //public SDBeesMappedEntityDefinition(SDBeesEntityDefinitionId entityDefinitionId, SDBeesExternalEntityInfo externalEntityInfo, SDBeesEntityFilter entityFilter,
        //    HashSet<SDBeesMappedPropertyDefinition> propertyMappings, HashSet<SDBeesRelationshipDefinitionId> requiredSourceRelations, HashSet<SDBeesRelationshipDefinitionId> requiredTargetRelations)
        //{
        //    EntityDefinitionId = entityDefinitionId;
        //    m_pluginEntityInfo = externalEntityInfo;
        //    m_propertyMappings = propertyMappings;
        //    m_filter = entityFilter;
        //    m_requiredSourceRelationships = requiredSourceRelations;
        //    m_requiredTargetRelationships = requiredTargetRelations;
        //}


		private SDBeesEntityDefinitionId m_entityDefinitionId;
        /// <summary>
        /// Die SDBees Entity Definition
        /// </summary>
        [DataMember]
		public SDBeesEntityDefinitionId EntityDefinitionId
		{
            get { return m_entityDefinitionId; }
            set { m_entityDefinitionId = value; }
		}

		private SDBeesExternalEntityInfo m_pluginEntityInfo;
        /// <summary>
        /// Das gemappte Plugin Entity
        /// </summary>
        [DataMember]
		public SDBeesExternalEntityInfo PluginEntityInfo
		{
            get { return m_pluginEntityInfo; }
            set { m_pluginEntityInfo = value; }
		}

		HashSet<SDBeesMappedPropertyDefinition> m_propertyMappings;
        /// <summary>
        /// die zugeordenten Eigenschafts Mappings
        /// </summary>
        [DataMember]
		public HashSet<SDBeesMappedPropertyDefinition> PropertyMappings
		{
            get { return m_propertyMappings; }
            set { m_propertyMappings = value; }
		}

		private SDBeesEntityFilter m_filter;
        /// <summary>
        /// Der Filter für das Entity Mapping
        /// </summary>
        [DataMember]
		public SDBeesEntityFilter filter
		{
            get { return m_filter; }
            set { m_filter = value; }
		}

        HashSet<SDBeesRelationshipDefinitionId> m_requiredSourceRelationships;
        /// <summary>
        /// Gemappte Quell Beziehungen
        /// </summary>
        [DataMember]
        public HashSet<SDBeesRelationshipDefinitionId> RequiredSourceRelationships
        {
            get { return m_requiredSourceRelationships; }
            set { m_requiredSourceRelationships = value; }
        }

        HashSet<SDBeesRelationshipDefinitionId> m_requiredTargetRelationships;
        /// <summary>
        /// Gemappte Ziel Beziehungen
        /// </summary>
        [DataMember]
        public HashSet<SDBeesRelationshipDefinitionId> RequiredTargetRelationships
        {
            get { return m_requiredTargetRelationships; }
            set { m_requiredTargetRelationships = value; }
        }

        private SDBeesMappedEntityDefinitionId m_mappedEntityDefinitionId;
        /// <summary>
        /// Die SDBees Mapped Entity Definition
        /// </summary>
        [DataMember]
        public SDBeesMappedEntityDefinitionId MappedEntityDefinitionId
        {
            get { return m_mappedEntityDefinitionId; }
            set { m_mappedEntityDefinitionId = value; }
        }

    }
}


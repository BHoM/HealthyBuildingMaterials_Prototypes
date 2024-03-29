/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2024, the respective contributors. All rights reserved.
 *
 * Each contributor holds copyright over their respective contributions.
 * The project versioning (Git) records all such contribution source information.
 *                                           
 *                                                                              
 * The BHoM is free software: you can redistribute it and/or modify         
 * it under the terms of the GNU Lesser General Public License as published by  
 * the Free Software Foundation, either version 3.0 of the License, or          
 * (at your option) any later version.                                          
 *                                                                              
 * The BHoM is distributed in the hope that it will be useful,              
 * but WITHOUT ANY WARRANTY; without even the implied warranty of               
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the                 
 * GNU Lesser General Public License for more details.                          
 *                                                                            
 * You should have received a copy of the GNU Lesser General Public License     
 * along with this code. If not, see <https://www.gnu.org/licenses/lgpl-3.0.html>.      
 */

using BH.oM.Base;
using System.ComponentModel;
using BH.oM.Physical.Materials;
using System.Collections.Generic;

namespace BH.oM.HealthyBuildingMaterials
{
    [Description("BHoM Healthly Product Declarations are a currated list of properties which define an HPD that can be integrated within standard workflows. More information on the HPD methodology along with all details can be found on the organisation's website 'https://www.hpd-collaborative.org/hpd-open-standard-all-versions/'")]
    public class HealthProductDeclaration : BHoMObject, IMaterialProperties
    {
        /***************************************************/
        /**** Properties                                ****/
        /***************************************************/

        [Description("Container for all health metric data")]
        public virtual List<HealthMetric> HealthMetric { get; set; } = null;

        [Description("Does this HPD have a certification?")]
        public virtual bool Certified { get; set; } = false;

        [Description("The Type of Health Product Declaration")]
        public virtual HPDType Type { get; set; } = HPDType.Undefined;

        /***************************************************/
    }
}


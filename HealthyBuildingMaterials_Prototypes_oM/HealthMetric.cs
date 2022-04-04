/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2022, the respective contributors. All rights reserved.
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

using System.Collections.Generic;
using BH.oM.Base;
using System.ComponentModel;

namespace BH.oM.HealthyBuildingMaterials
{
    [Description("I need to add a description here.")]
    public class HealthMetric : BHoMObject
    {
        /***************************************************/
        /**** Properties                                ****/
        /***************************************************/

        [Description("An Environmental Impact Metric filter used to classify the Environmental Metric that is being stored on the EPD.")]
        public virtual HealthProductDeclarationField Field { get; set; } = HealthProductDeclarationField.Undefined;

        [Description("Phase of life abbreviation for the scope of the EPD. A single EnvironmentalMetric can contain either a single Phase or a list of Phases i.e. A1, A2, A3.")]
        public virtual List<HealthyBuildingColors> Colors { get; set; } = new List<HealthyBuildingColors>();

        [Description("The amount of the specified Field.")]
        public virtual double Quantity { get; set; } = 0;

        /***************************************************/
    }
}


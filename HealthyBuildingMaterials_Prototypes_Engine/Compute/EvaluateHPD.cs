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

using BH.Engine.Matter;
using BH.oM.Base;
using BH.oM.Base.Attributes;
using BH.oM.Dimensional;
using BH.oM.HealthyBuildingMaterials;
using BH.oM.HealthyBuildingMaterials.Fragments;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BH.Engine.HealthyBuildingMaterials
{
    public static partial class Compute
    {
        /***************************************************/
        /****   Public Methods                          ****/
        /***************************************************/

        [Description("This method calculates the quantity of a supplied metric by querying Health Impact Metrics from the HPD materialFragment and the object's mass.")]
        [Input("elementM", "An IElementM object used to calculate HPD metric.")]
        [Output("quantity", "The total quantity of the desired metric based on the HPD.")]
        public static double EvaluateHPD(IElementM elementM, HealthProductDeclarationField field = HealthProductDeclarationField.Undefined)
        {
            double volume = elementM.ISolidVolume();

            List<IFragment> fragments = BH.Engine.Base.Query.GetAllFragments((IBHoMObject)elementM);

            HPDDensity densityFragment = (HPDDensity)fragments.Where(x => typeof(HPDDensity).IsAssignableFrom(x.GetType())).FirstOrDefault();

            double density = densityFragment.Density;

            double mass = volume * density;

            if (elementM == null)
            {
                BH.Engine.Base.Compute.RecordError("No IElementM was provided.");
            }

            BH.oM.Physical.Materials.MaterialComposition mc = elementM.IMaterialComposition();

            HealthProductDeclaration hpd = (HealthProductDeclaration)mc.Materials.Select(x => x.Properties.Where(y => y is HealthProductDeclaration).FirstOrDefault() as HealthProductDeclaration);

            // grab the metric quantity 

            IEnumerable<HealthMetric> filteredMetrics = hpd.HealthMetric.Where(x => x.Field == field);
            if (filteredMetrics.Count() == 0)
            {
                BH.Engine.Base.Compute.RecordError("No metrics of the specified Field could be found.");
                return double.NaN;
            }

            if (filteredMetrics.Count() == 0)
            {
                BH.Engine.Base.Compute.RecordError("No Health Metrics could be found.");
                return double.NaN;
            }

            double metricQuantity = filteredMetrics.Select(x => x.Quantity).FirstOrDefault();

            return mass * metricQuantity;
        }
    }
}
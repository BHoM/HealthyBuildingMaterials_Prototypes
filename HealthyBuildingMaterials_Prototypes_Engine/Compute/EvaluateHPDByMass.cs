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

using BH.oM.Base.Attributes;
using BH.oM.HealthyBuildingMaterials;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BH.Engine.Reflection;

namespace BH.Engine.HealthyBuildingMaterials
{
    public static partial class Compute
    {
        /***************************************************/
        /****   Public Methods                          ****/
        /***************************************************/

        // NOTE: this method references the HPD created in the LCA toolkit instead of the one included in this namespace and should be resolved. 

        [Description("This method calculates the quantity of a supplied metric by querying Health Impact Metrics from the HPD materialFragment and the object's mass.")]
        [Input("hpd", "A Healthy Building Material.")]
        [Input("masses", "A list of masses from the elements to be evaluated.")]
        [Input("color", "A color from the HPD to be evaluated.")]
        [Output("quantities", "The product of each metric being evaluated.")]
        public static List<double> EvaluateHPDByMass(HealthProductDeclaration hpd, HealthyBuildingColors color, double mass)
        {
            List<double> results = new List<double>();

            if (hpd == null)
            {
                BH.Engine.Base.Compute.RecordError("No HPD data was provided.");
                return new List<double>();
            }

            // create a list of properties as strings to check the field against for matching
            List<string> metricNames = Reflection.Query.PropertyNames(hpd).ToList();

            if (metricNames.Count > 0)
            {
                for (int i = 0; i <= metricNames.Count; i++)
                {
                    // I don't love this, but it eliminates the first few properties that aren't relevant
                    if (metricNames[i] == "Colors" || metricNames[i] == "Certified" || metricNames[i] == "Type")
                    {
                        continue;
                    }
                    // stop if you hit the last relevant property
                    if (metricNames[i] == "BHoM_Guid")
                    {
                        return results;
                    }
                    double metricValue = (double)Base.Query.PropertyValue(hpd, metricNames[i]);
                    double result = mass * metricValue;
                    results.Add(result);
                }
                return results;
            } else
            {
                BH.Engine.Base.Compute.RecordError("You have not provided a valid HPD object.");
            }

            if (results.Count <= 0)
            {
                Base.Compute.RecordNote("The results are empty. Please check inputs and try again.");
            }
            return new List<double>();
        }
    }
}
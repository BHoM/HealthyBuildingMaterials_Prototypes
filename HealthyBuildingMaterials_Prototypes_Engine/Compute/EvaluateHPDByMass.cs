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
using BH.oM.LifeCycleAssessment;
using System;

namespace BH.Engine.HealthyBuildingMaterials
{
    public static partial class Compute
    {
        /***************************************************/
        /****   Public Methods                          ****/
        /***************************************************/

        [Description("This method calculates the quantity of a supplied metric by querying Health Impact Metrics from the HPD materialFragment and the object's mass.")]
        [Input("HPD", "An HPD dataset.")]
        [Input("masses", "A list of masses from the elements to be evaluated.")]
        [Input("field", "The specific health metric field you would like to evaluate.")]
        [Output("quantity", "The total quantity of the desired metric based on the HPD.")]
        public static double EvaluateHPDByMass(BH.oM.LifeCycleAssessment.HealthProductDeclaration healthProductDeclartion, double mass, HealthProductDeclarationFieldOld field = HealthProductDeclarationFieldOld.Undefined)
        {
            if (healthProductDeclartion == null)
            {
                BH.Engine.Base.Compute.RecordError("No HPD data was provided.");
                return 0;
            }

            // create a list of strings to check the field against

            List<string> metricNames = Base.Query.GetAllPropertyFullNames(healthProductDeclartion).ToList();

            double metricValue = 0;

            if (metricNames.Contains("BH.oM.LifeCycleAssessment.HealthProductDeclaration." + field.ToString()))
            {
                metricValue = (double)healthProductDeclartion.GetType().GetProperty(field.ToString()).GetValue(healthProductDeclartion);

                if (double.IsNaN(metricValue))
                {
                    Engine.Base.Compute.RecordWarning("NaN value detected for the corresponding field selection.");
                    metricValue = double.NaN;
                }

            } else
            {
                BH.Engine.Base.Compute.RecordError("No property could be found within the HPD dataset that matches the field selection.");
            }

            return mass * metricValue;
        }
    }
}
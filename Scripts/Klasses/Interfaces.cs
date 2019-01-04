using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Contains the nessary informations and methods about one Inhabitant for the management
/// </summary>
public interface IInhabitant
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>The name.</value>
    String Name { get; }

    /// <summary>
    /// Return the total tax for this Inhabitant
    /// </summary>
    /// <value>The tax.</value>
    float Tax { get; }
}

/// <summary>
/// Contains the nessary informations and methods about one Duke for the management
/// </summary>
public interface IDuke: IInhabitant
{
    /// <summary>
    /// Receive the information since how many years he/she is the leader.
    /// </summary>
    /// <value>The leader since.</value>
    int LeaderSince { get; }
}

/// <summary>
/// Management the tax
/// </summary>
public interface IManagement
{
    /// <summary>
    /// Return the complete Tax amount
    /// </summary>
    /// <returns>The tax amount.</returns>
    float TotalTax();

    /// <summary>
    /// Sets the base tax for the calculation 
    /// </summary>
    /// <value>The dwarf tax.</value>
    float BaseTax { set; }

    /// <summary>
    /// Return all inhabitants.
    /// </summary>
    /// <returns>The inhabitants.</returns>
    List<IInhabitant> AllInhabitants();

    /// <summary>
    /// Return all dukes.
    /// </summary>
    /// <returns>The dukes.</returns>
    List<IDuke> AllDukes();
}

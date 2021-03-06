/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//-----------------------------------NOTICE----------------------------------//
// Some of this file is automatically filled in by a Python script.  Only    //
// add custom code in the designated areas or it will be overwritten during  //
// the next update.                                                          //
//-----------------------------------NOTICE----------------------------------//

using System;
using System.IO;
using System.Collections.Generic;


namespace Niflib {

/*! A box. */
public class bhkBoxShape : bhkConvexShape {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkBoxShape", bhkConvexShape.TYPE);
	/*! Not used. The following wants to be aligned at 16 bytes. */
	internal Array8<byte> unused;
	/*!
	 * A cube stored in Half Extents. A unit cube (1.0, 1.0, 1.0) would be stored as
	 * 0.5, 0.5, 0.5.
	 */
	internal Vector3 dimensions;
	/*!
	 * Unused as Havok stores the Half Extents as hkVector4 with the W component
	 * unused.
	 */
	internal float unusedFloat;

	public bhkBoxShape() {
	unusedFloat = 0.0f;
}

/*!
 * Used to determine the type of a particular instance of this object.
 * \return The type constant for the actual type of the object.
 */
public override Type_ GetType() => TYPE;

/*!
 * A factory function used during file reading to create an instance of this type of object.
 * \return A pointer to a newly allocated instance of this type of object.
 */
public static NiObject Create() => new bhkBoxShape();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	for (var i1 = 0; i1 < 8; i1++) {
		Nif.NifStream(out unused[i1], s, info);
	}
	Nif.NifStream(out dimensions, s, info);
	Nif.NifStream(out unusedFloat, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	for (var i1 = 0; i1 < 8; i1++) {
		Nif.NifStream(unused[i1], s, info);
	}
	Nif.NifStream(dimensions, s, info);
	Nif.NifStream(unusedFloat, s, info);

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	uint array_output_count = 0;
	s.Append(base.AsString());
	array_output_count = 0;
	for (var i1 = 0; i1 < 8; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Unused[{i1}]:  {unused[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Dimensions:  {dimensions}");
	s.AppendLine($"  Unused Float:  {unusedFloat}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}

//--BEGIN:FILE FOOT--//
/*!
* Gets or sets the dimensions of the box.
* \param value The new dimensions for the bounding box.
*/
public Vector3 Dimensions
{
    get => dimensions;
    set
    {
        dimensions = value;
        //minimumSize = Math.Min(Math.Min(value.x, value.y), value.z);
    }
}

/*! Helper routine for calculating mass properties.
*  \param[in]  density Uniform density of object
*  \param[in]  solid Determines whether the object is assumed to be solid or not
*  \param[out] mass Calculated mass of the object
*  \param[out] center Center of mass
*  \param[out] inertia Mass Inertia Tensor
*  \return Return mass, center, and inertia tensor.
*/
public virtual void CalcMassProperties(float density, bool solid, out float mass, out float volume, out Vector3 center, out InertiaMatrix inertia) => Inertia.CalcMassPropertiesBox(dimensions, density, solid, mass, volume, center, inertia);
//--END:CUSTOM--//

}

}
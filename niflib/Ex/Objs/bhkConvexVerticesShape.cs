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

/*!
 * A convex shape built from vertices. Note that if the shape is used in
 *         a non-static object (such as clutter), then they will simply fall
 *         through ground when they are under a bhkListShape.
 */
public class bhkConvexVerticesShape : bhkConvexShape {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkConvexVerticesShape", bhkConvexShape.TYPE);
	/*!  */
	public hkWorldObjCinfoProperty verticesProperty;
	/*!  */
	public hkWorldObjCinfoProperty normalsProperty;
	/*! Number of vertices. */
	public uint numVertices;
	/*! Vertices. Fourth component is 0. Lexicographically sorted. */
	public Vector4[] vertices;
	/*! The number of half spaces. */
	public uint numNormals;
	/*!
	 * Half spaces as determined by the set of vertices above. First three components
	 * define the normal pointing to the exterior, fourth component is the signed
	 * distance of the separating plane to the origin: it is minus the dot product of v
	 * and n, where v is any vertex on the separating plane, and n is the normal.
	 * Lexicographically sorted.
	 */
	public Vector4[] normals;

	public bhkConvexVerticesShape() {
	numVertices = (uint)0;
	numNormals = (uint)0;
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
public static NiObject Create() => new bhkConvexVerticesShape();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out verticesProperty.data, s, info);
	Nif.NifStream(out verticesProperty.size, s, info);
	Nif.NifStream(out verticesProperty.capacityAndFlags, s, info);
	Nif.NifStream(out normalsProperty.data, s, info);
	Nif.NifStream(out normalsProperty.size, s, info);
	Nif.NifStream(out normalsProperty.capacityAndFlags, s, info);
	Nif.NifStream(out numVertices, s, info);
	vertices = new Vector4[numVertices];
	for (var i1 = 0; i1 < vertices.Length; i1++) {
		Nif.NifStream(out vertices[i1], s, info);
	}
	Nif.NifStream(out numNormals, s, info);
	normals = new Vector4[numNormals];
	for (var i1 = 0; i1 < normals.Length; i1++) {
		Nif.NifStream(out normals[i1], s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numNormals = (uint)normals.Length;
	numVertices = (uint)vertices.Length;
	Nif.NifStream(verticesProperty.data, s, info);
	Nif.NifStream(verticesProperty.size, s, info);
	Nif.NifStream(verticesProperty.capacityAndFlags, s, info);
	Nif.NifStream(normalsProperty.data, s, info);
	Nif.NifStream(normalsProperty.size, s, info);
	Nif.NifStream(normalsProperty.capacityAndFlags, s, info);
	Nif.NifStream(numVertices, s, info);
	for (var i1 = 0; i1 < vertices.Length; i1++) {
		Nif.NifStream(vertices[i1], s, info);
	}
	Nif.NifStream(numNormals, s, info);
	for (var i1 = 0; i1 < normals.Length; i1++) {
		Nif.NifStream(normals[i1], s, info);
	}

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string asString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	uint array_output_count = 0;
	s.Append(base.asString());
	numNormals = (uint)normals.Length;
	numVertices = (uint)vertices.Length;
	s.AppendLine($"  Data:  {verticesProperty.data}");
	s.AppendLine($"  Size:  {verticesProperty.size}");
	s.AppendLine($"  Capacity and Flags:  {verticesProperty.capacityAndFlags}");
	s.AppendLine($"  Data:  {normalsProperty.data}");
	s.AppendLine($"  Size:  {normalsProperty.size}");
	s.AppendLine($"  Capacity and Flags:  {normalsProperty.capacityAndFlags}");
	s.AppendLine($"  Num Vertices:  {numVertices}");
	array_output_count = 0;
	for (var i1 = 0; i1 < vertices.Length; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Vertices[{i1}]:  {vertices[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Normals:  {numNormals}");
	array_output_count = 0;
	for (var i1 = 0; i1 < normals.Length; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Normals[{i1}]:  {normals[i1]}");
		array_output_count++;
	}
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


}

}
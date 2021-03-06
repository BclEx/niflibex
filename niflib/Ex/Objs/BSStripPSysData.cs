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

/*! Bethesda-Specific (mesh?) Particle System Data. */
public class BSStripPSysData : NiPSysData {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("BSStripPSysData", NiPSysData.TYPE);
	/*!  */
	internal ushort maxPointCount;
	/*!  */
	internal float startCapSize;
	/*!  */
	internal float endCapSize;
	/*!  */
	internal bool doZPrepass;

	public BSStripPSysData() {
	maxPointCount = (ushort)0;
	startCapSize = 0.0f;
	endCapSize = 0.0f;
	doZPrepass = false;
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
public static NiObject Create() => new BSStripPSysData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out maxPointCount, s, info);
	Nif.NifStream(out startCapSize, s, info);
	Nif.NifStream(out endCapSize, s, info);
	Nif.NifStream(out doZPrepass, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(maxPointCount, s, info);
	Nif.NifStream(startCapSize, s, info);
	Nif.NifStream(endCapSize, s, info);
	Nif.NifStream(doZPrepass, s, info);

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Max Point Count:  {maxPointCount}");
	s.AppendLine($"  Start Cap Size:  {startCapSize}");
	s.AppendLine($"  End Cap Size:  {endCapSize}");
	s.AppendLine($"  Do Z Prepass:  {doZPrepass}");
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
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

/*! Rotating particles data object. */
public class NiRotatingParticlesData : NiParticlesData {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiRotatingParticlesData", NiParticlesData.TYPE);
	/*! Is the particle rotation array present? */
	internal bool hasRotations2;
	/*! The individual particle rotations. */
	internal IList<Quaternion> rotations2;

	public NiRotatingParticlesData() {
	hasRotations2 = false;
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
public static NiObject Create() => new NiRotatingParticlesData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	if (info.version <= 0x04020200) {
		Nif.NifStream(out hasRotations2, s, info);
		if (hasRotations2) {
			rotations2 = new Quaternion[numVertices];
			for (var i3 = 0; i3 < rotations2.Count; i3++) {
				Nif.NifStream(out rotations2[i3], s, info);
			}
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	if (info.version <= 0x04020200) {
		Nif.NifStream(hasRotations2, s, info);
		if (hasRotations2) {
			for (var i3 = 0; i3 < rotations2.Count; i3++) {
				Nif.NifStream(rotations2[i3], s, info);
			}
		}
	}

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
	s.AppendLine($"  Has Rotations 2:  {hasRotations2}");
	if (hasRotations2) {
		array_output_count = 0;
		for (var i2 = 0; i2 < rotations2.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Rotations 2[{i2}]:  {rotations2[i2]}");
			array_output_count++;
		}
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
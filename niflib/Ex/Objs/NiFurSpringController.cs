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

/*!  */
public class NiFurSpringController : NiTimeController {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiFurSpringController", NiTimeController.TYPE);
	/*! Unknown. */
	internal float unknownFloat;
	/*! Unknown. */
	internal float unknownFloat2;
	/*! The number of node bones referenced as influences. */
	internal uint numBones;
	/*! List of all armature bones. */
	internal IList<NiNode> bones;
	/*! The number of node bones referenced as influences. */
	internal uint numBones2;
	/*! List of all armature bones. */
	internal IList<NiNode> bones2;

	public NiFurSpringController() {
	unknownFloat = 0.0f;
	unknownFloat2 = 0.0f;
	numBones = (uint)0;
	numBones2 = (uint)0;
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
public static NiObject Create() => new NiFurSpringController();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out unknownFloat, s, info);
	Nif.NifStream(out unknownFloat2, s, info);
	Nif.NifStream(out numBones, s, info);
	bones = new *[numBones];
	for (var i1 = 0; i1 < bones.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out numBones2, s, info);
	bones2 = new *[numBones2];
	for (var i1 = 0; i1 < bones2.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numBones2 = (uint)bones2.Count;
	numBones = (uint)bones.Count;
	Nif.NifStream(unknownFloat, s, info);
	Nif.NifStream(unknownFloat2, s, info);
	Nif.NifStream(numBones, s, info);
	for (var i1 = 0; i1 < bones.Count; i1++) {
		WriteRef((NiObject)bones[i1], s, info, link_map, missing_link_stack);
	}
	Nif.NifStream(numBones2, s, info);
	for (var i1 = 0; i1 < bones2.Count; i1++) {
		WriteRef((NiObject)bones2[i1], s, info, link_map, missing_link_stack);
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
	numBones2 = (uint)bones2.Count;
	numBones = (uint)bones.Count;
	s.AppendLine($"  Unknown Float:  {unknownFloat}");
	s.AppendLine($"  Unknown Float 2:  {unknownFloat2}");
	s.AppendLine($"  Num Bones:  {numBones}");
	array_output_count = 0;
	for (var i1 = 0; i1 < bones.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Bones[{i1}]:  {bones[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Bones 2:  {numBones2}");
	array_output_count = 0;
	for (var i1 = 0; i1 < bones2.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Bones 2[{i1}]:  {bones2[i1]}");
		array_output_count++;
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < bones.Count; i1++) {
		bones[i1] = FixLink<NiNode>(objects, link_stack, missing_link_stack, info);
	}
	for (var i1 = 0; i1 < bones2.Count; i1++) {
		bones2[i1] = FixLink<NiNode>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < bones.Count; i1++) {
	}
	for (var i1 = 0; i1 < bones2.Count; i1++) {
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < bones.Count; i1++) {
		if (bones[i1] != null)
			ptrs.Add((NiObject)bones[i1]);
	}
	for (var i1 = 0; i1 < bones2.Count; i1++) {
		if (bones2[i1] != null)
			ptrs.Add((NiObject)bones2[i1]);
	}
	return ptrs;
}


}

}
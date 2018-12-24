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

/*! Unknown */
public class NiEnvMappedTriShape : NiObjectNET {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiEnvMappedTriShape", NiObjectNET.TYPE);
	/*! unknown (=4 - 5) */
	public ushort unknown1;
	/*! unknown */
	public Matrix44 unknownMatrix;
	/*! The number of child objects. */
	public uint numChildren;
	/*! List of child node object indices. */
	public NiAVObject[] children;
	/*! unknown */
	public NiObject child2;
	/*! unknown */
	public NiObject child3;

	public NiEnvMappedTriShape() {
	unknown1 = (ushort)0;
	numChildren = (uint)0;
	child2 = null;
	child3 = null;
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
public static NiObject Create() => new NiEnvMappedTriShape();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out unknown1, s, info);
	Nif.NifStream(out unknownMatrix, s, info);
	Nif.NifStream(out numChildren, s, info);
	children = new Ref[numChildren];
	for (var i1 = 0; i1 < children.Length; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numChildren = (uint)children.Length;
	Nif.NifStream(unknown1, s, info);
	Nif.NifStream(unknownMatrix, s, info);
	Nif.NifStream(numChildren, s, info);
	for (var i1 = 0; i1 < children.Length; i1++) {
		WriteRef((NiObject)children[i1], s, info, link_map, missing_link_stack);
	}
	WriteRef((NiObject)child2, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)child3, s, info, link_map, missing_link_stack);

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
	numChildren = (uint)children.Length;
	s.AppendLine($"  Unknown 1:  {unknown1}");
	s.AppendLine($"  Unknown Matrix:  {unknownMatrix}");
	s.AppendLine($"  Num Children:  {numChildren}");
	array_output_count = 0;
	for (var i1 = 0; i1 < children.Length; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Children[{i1}]:  {children[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Child 2:  {child2}");
	s.AppendLine($"  Child 3:  {child3}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < children.Length; i1++) {
		children[i1] = FixLink<NiAVObject>(objects, link_stack, missing_link_stack, info);
	}
	child2 = FixLink<NiObject>(objects, link_stack, missing_link_stack, info);
	child3 = FixLink<NiObject>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < children.Length; i1++) {
		if (children[i1] != null)
			refs.Add((NiObject)children[i1]);
	}
	if (child2 != null)
		refs.Add((NiObject)child2);
	if (child3 != null)
		refs.Add((NiObject)child3);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < children.Length; i1++) {
	}
	return ptrs;
}


}

}
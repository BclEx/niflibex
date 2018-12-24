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
public class NiSkinningMeshModifier : NiMeshModifier {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiSkinningMeshModifier", NiMeshModifier.TYPE);
	/*!
	 * USE_SOFTWARE_SKINNING = 0x0001
	 *             RECOMPUTE_BOUNDS = 0x0002
	 */
	public ushort flags;
	/*! The root bone of the skeleton. */
	public NiAVObject skeletonRoot;
	/*!
	 * The transform that takes the root bone parent coordinate system into the skin
	 * coordinate system.
	 */
	public NiTransform skeletonTransform;
	/*! The number of bones referenced by this mesh modifier. */
	public uint numBones;
	/*! Pointers to the bone nodes that affect this skin. */
	public NiAVObject[] bones;
	/*! The transforms that go from bind-pose space to bone space. */
	public NiTransform[] boneTransforms;
	/*! The bounds of the bones.  Only stored if the RECOMPUTE_BOUNDS bit is set. */
	public NiBound[] boneBounds;

	public NiSkinningMeshModifier() {
	flags = (ushort)0;
	skeletonRoot = null;
	numBones = (uint)0;
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
public static NiObject Create() => new NiSkinningMeshModifier();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out flags, s, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out skeletonTransform.rotation, s, info);
	Nif.NifStream(out skeletonTransform.translation, s, info);
	Nif.NifStream(out skeletonTransform.scale, s, info);
	Nif.NifStream(out numBones, s, info);
	bones = new *[numBones];
	for (var i1 = 0; i1 < bones.Length; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	boneTransforms = new NiTransform[numBones];
	for (var i1 = 0; i1 < boneTransforms.Length; i1++) {
		Nif.NifStream(out boneTransforms[i1].rotation, s, info);
		Nif.NifStream(out boneTransforms[i1].translation, s, info);
		Nif.NifStream(out boneTransforms[i1].scale, s, info);
	}
	if (((flags & 2) != 0)) {
		boneBounds = new NiBound[numBones];
		for (var i2 = 0; i2 < boneBounds.Length; i2++) {
			Nif.NifStream(out boneBounds[i2].center, s, info);
			Nif.NifStream(out boneBounds[i2].radius, s, info);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numBones = (uint)bones.Length;
	Nif.NifStream(flags, s, info);
	WriteRef((NiObject)skeletonRoot, s, info, link_map, missing_link_stack);
	Nif.NifStream(skeletonTransform.rotation, s, info);
	Nif.NifStream(skeletonTransform.translation, s, info);
	Nif.NifStream(skeletonTransform.scale, s, info);
	Nif.NifStream(numBones, s, info);
	for (var i1 = 0; i1 < bones.Length; i1++) {
		WriteRef((NiObject)bones[i1], s, info, link_map, missing_link_stack);
	}
	for (var i1 = 0; i1 < boneTransforms.Length; i1++) {
		Nif.NifStream(boneTransforms[i1].rotation, s, info);
		Nif.NifStream(boneTransforms[i1].translation, s, info);
		Nif.NifStream(boneTransforms[i1].scale, s, info);
	}
	if (((flags & 2) != 0)) {
		for (var i2 = 0; i2 < boneBounds.Length; i2++) {
			Nif.NifStream(boneBounds[i2].center, s, info);
			Nif.NifStream(boneBounds[i2].radius, s, info);
		}
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
	numBones = (uint)bones.Length;
	s.AppendLine($"  Flags:  {flags}");
	s.AppendLine($"  Skeleton Root:  {skeletonRoot}");
	s.AppendLine($"  Rotation:  {skeletonTransform.rotation}");
	s.AppendLine($"  Translation:  {skeletonTransform.translation}");
	s.AppendLine($"  Scale:  {skeletonTransform.scale}");
	s.AppendLine($"  Num Bones:  {numBones}");
	array_output_count = 0;
	for (var i1 = 0; i1 < bones.Length; i1++) {
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
	array_output_count = 0;
	for (var i1 = 0; i1 < boneTransforms.Length; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Rotation:  {boneTransforms[i1].rotation}");
		s.AppendLine($"    Translation:  {boneTransforms[i1].translation}");
		s.AppendLine($"    Scale:  {boneTransforms[i1].scale}");
	}
	if (((flags & 2) != 0)) {
		array_output_count = 0;
		for (var i2 = 0; i2 < boneBounds.Length; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			s.AppendLine($"      Center:  {boneBounds[i2].center}");
			s.AppendLine($"      Radius:  {boneBounds[i2].radius}");
		}
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	skeletonRoot = FixLink<NiAVObject>(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < bones.Length; i1++) {
		bones[i1] = FixLink<NiAVObject>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < bones.Length; i1++) {
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	if (skeletonRoot != null)
		ptrs.Add((NiObject)skeletonRoot);
	for (var i1 = 0; i1 < bones.Length; i1++) {
		if (bones[i1] != null)
			ptrs.Add((NiObject)bones[i1]);
	}
	return ptrs;
}


}

}
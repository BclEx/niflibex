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

/*! NiLookAtInterpolator rotates an object so that it always faces a target object. */
public class NiLookAtInterpolator : NiInterpolator {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiLookAtInterpolator", NiInterpolator.TYPE);
	/*!  */
	internal LookAtFlags flags;
	/*!  */
	internal NiNode lookAt;
	/*!  */
	internal IndexString lookAtName;
	/*!  */
	internal NiQuatTransform transform;
	/*!  */
	internal NiPoint3Interpolator interpolator_Translation;
	/*!  */
	internal NiFloatInterpolator interpolator_Roll;
	/*!  */
	internal NiFloatInterpolator interpolator_Scale;

	public NiLookAtInterpolator() {
	flags = (LookAtFlags)0;
	lookAt = null;
	interpolator_Translation = null;
	interpolator_Roll = null;
	interpolator_Scale = null;
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
public static NiObject Create() => new NiLookAtInterpolator();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out flags, s, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out lookAtName, s, info);
	if (info.version <= 0x1404000C) {
		Nif.NifStream(out transform.translation, s, info);
		Nif.NifStream(out transform.rotation, s, info);
		Nif.NifStream(out transform.scale, s, info);
		if (info.version <= 0x0A01006D) {
			for (var i3 = 0; i3 < 3; i3++) {
				{
					bool tmp;
					Nif.NifStream(out tmp, s, info);
					transform.trsValid[i3] = tmp;
				}
			}
		}
	}
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(flags, s, info);
	WriteRef((NiObject)lookAt, s, info, link_map, missing_link_stack);
	Nif.NifStream(lookAtName, s, info);
	if (info.version <= 0x1404000C) {
		Nif.NifStream(transform.translation, s, info);
		Nif.NifStream(transform.rotation, s, info);
		Nif.NifStream(transform.scale, s, info);
		if (info.version <= 0x0A01006D) {
			for (var i3 = 0; i3 < 3; i3++) {
				{
					bool tmp = transform.trsValid[i3];
					Nif.NifStream(tmp, s, info);
				}
			}
		}
	}
	WriteRef((NiObject)interpolator_Translation, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)interpolator_Roll, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)interpolator_Scale, s, info, link_map, missing_link_stack);

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
	s.AppendLine($"  Flags:  {flags}");
	s.AppendLine($"  Look At:  {lookAt}");
	s.AppendLine($"  Look At Name:  {lookAtName}");
	s.AppendLine($"  Translation:  {transform.translation}");
	s.AppendLine($"  Rotation:  {transform.rotation}");
	s.AppendLine($"  Scale:  {transform.scale}");
	array_output_count = 0;
	for (var i1 = 0; i1 < 3; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    TRS Valid[{i1}]:  {transform.trsValid[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Interpolator: Translation:  {interpolator_Translation}");
	s.AppendLine($"  Interpolator: Roll:  {interpolator_Roll}");
	s.AppendLine($"  Interpolator: Scale:  {interpolator_Scale}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	lookAt = FixLink<NiNode>(objects, link_stack, missing_link_stack, info);
	interpolator_Translation = FixLink<NiPoint3Interpolator>(objects, link_stack, missing_link_stack, info);
	interpolator_Roll = FixLink<NiFloatInterpolator>(objects, link_stack, missing_link_stack, info);
	interpolator_Scale = FixLink<NiFloatInterpolator>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (interpolator_Translation != null)
		refs.Add((NiObject)interpolator_Translation);
	if (interpolator_Roll != null)
		refs.Add((NiObject)interpolator_Roll);
	if (interpolator_Scale != null)
		refs.Add((NiObject)interpolator_Scale);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	if (lookAt != null)
		ptrs.Add((NiObject)lookAt);
	return ptrs;
}

//--BEGIN:FILE FOOT--//
        /*!
         * Gets or sets the node that this interpolator will focus on.
         * \return The new node that the interpolator will focus on, or NULL to clear the current one.
         */
        public NiNode LookAt
        {
            get => lookAt;
            set => lookAt = value;
        }

        /*!
         * Gets or sets the translation of the interpolator.  Could be the necessary value to point at the referenced node in the current pose.
         * \param[in] value The new translation for the interpolator.
         */
        public Vector3 Translation
        {
            get => transform.translation;
            set => transform.translation = value;
        }

        /*!
         * Gets or sets the rotation of the interpolator.  Could be the necessary value to point at the referenced node in the current pose.
         * \param[in] value The new rotation for the interpolator.
         */
        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }

        /*!
         * Gets or sets the scale of the interpolator.  Could be the necessary value to point at the referenced node in the current pose.
         * \param[in] value The new scale for the interpolator.
         */
        public float Scale
        {
            get => transform.scale;
            set => transform.scale = value;
        }
//--END:CUSTOM--//

}

}
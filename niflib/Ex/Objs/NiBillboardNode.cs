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
 * These nodes will always be rotated to face the camera creating a billboard
 * effect for any attached objects.
 * 
 *         In pre-10.1.0.0 the Flags field is used for BillboardMode.
 *         Bit 0: hidden
 *         Bits 1-2: collision mode
 *         Bit 3: unknown (set in most official meshes)
 *         Bits 5-6: billboard mode
 * 
 *         Collision modes:
 *         00 NONE
 *         01 USE_TRIANGLES
 *         10 USE_OBBS
 *         11 CONTINUE
 * 
 *         Billboard modes:
 *         00 ALWAYS_FACE_CAMERA
 *         01 ROTATE_ABOUT_UP
 *         10 RIGID_FACE_CAMERA
 *         11 ALWAYS_FACE_CENTER
 */
public class NiBillboardNode : NiNode {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiBillboardNode", NiNode.TYPE);
	/*! The way the billboard will react to the camera. */
	internal BillboardMode billboardMode;

	public NiBillboardNode() {
	billboardMode = (BillboardMode)0;
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
public static NiObject Create() => new NiBillboardNode();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	if (info.version >= 0x0A010000) {
		Nif.NifStream(out billboardMode, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	if (info.version >= 0x0A010000) {
		Nif.NifStream(billboardMode, s, info);
	}

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Billboard Mode:  {billboardMode}");
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
         * Gets or sets the bilboard mode of this bilboard node.  This determines how it will cause the node to face the camera.
         * \param[in] value The new bilboard mode.
         */
        public BillboardMode BillboardMode
        {
            get => billboardMode;
            set => billboardMode = value;
        }
//--END:CUSTOM--//

}

}
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

/*! Animates the visibility of an NiAVObject. */
public class NiVisController : NiBoolInterpController {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiVisController", NiBoolInterpController.TYPE);
	/*!  */
	internal NiVisData data;

	public NiVisController() {
	data = null;
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
public static NiObject Create() => new NiVisController();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	if (info.version <= 0x0A010067) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	if (info.version <= 0x0A010067) {
		WriteRef((NiObject)data, s, info, link_map, missing_link_stack);
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
	s.AppendLine($"  Data:  {data}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	if (info.version <= 0x0A010067) {
		data = FixLink<NiVisData>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (data != null)
		refs.Add((NiObject)data);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}

//--BEGIN:FILE FOOT--//
/*!
* Gets or sets the visibility data used by this controller.
* \param[in] n The new visibility data.
*/
public NiVisData Data
{
    get => data;
    set => data = value;
}

/*!
* This function will adjust the times in all the keys in the data objects
* referenced by this controller and any of its interpolators such that the
* phase will equal 0 and frequency will equal one.  In other words, it
* will cause the key times to be in seconds starting from zero.
*/
public virtual void NormalizeKeys()
{
    //Normalize any keys that are stored any NiVisData
    //Future NIF versions use BoolData to store this,
    //so no comparison to the Interpolator data is necessary.
    if (data != null)
        data.NormalizeKeys(phase, frequency);

    //Call the parent version of this function to finish up
    NiSingleInterpController.NormalizeKeys();
}
//--END:CUSTOM--//

}

}
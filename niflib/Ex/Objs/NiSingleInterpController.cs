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

/*! Uses a single NiInterpolator to animate its target value. */
public class NiSingleInterpController : NiInterpController {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiSingleInterpController", NiInterpController.TYPE);
	/*!  */
	internal NiInterpolator interpolator;

	public NiSingleInterpController() {
	interpolator = null;
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
public static NiObject Create() => new NiSingleInterpController();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	if (info.version >= 0x0A010068) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	if (info.version >= 0x0A010068) {
		WriteRef((NiObject)interpolator, s, info, link_map, missing_link_stack);
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
	s.AppendLine($"  Interpolator:  {interpolator}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	if (info.version >= 0x0A010068) {
		interpolator = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (interpolator != null)
		refs.Add((NiObject)interpolator);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}
        //--BEGIN:FILE FOOT--//
        /*!
         * Sets the interpolator used by this controller.
         * \param[in] value The new interpolator.
         */
        public NiInterpolator Interpolator
        {
            get => interpolator;
            set => interpolator = value;
        }

        /*!
         * This function will adjust the times in all the keys in the data objects
         * referenced by this controller and any of its interpolators such that the
         * phase will equal 0 and frequency will equal one.  In other words, it
         * will cause the key times to be in seconds starting from zero.
         */
        public virtual void NormalizeKeys()
        {
            //If this interpolator is key-based, normalize its keys
            var keyBased = interpolator as NiKeyBasedInterpolator;
            if (keyBased != null)
                keyBased.NormalizeKeys(phase, frequency);
            //Call the NiTimeController version of this function to normalize the start
            //and stop times and reset the phase and frequency
            NiTimeController.NormalizeKeys();
        }
        //--END:CUSTOM--//

    }

}
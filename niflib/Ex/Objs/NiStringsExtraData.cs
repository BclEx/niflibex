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

/*! List of strings; for example, a list of all bone names. */
public class NiStringsExtraData : NiExtraData {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiStringsExtraData", NiExtraData.TYPE);
	/*! Number of strings. */
	internal uint numStrings;
	/*! The strings. */
	internal IList<string> data;

	public NiStringsExtraData() {
	numStrings = (uint)0;
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
public static NiObject Create() => new NiStringsExtraData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out numStrings, s, info);
	data = new string[numStrings];
	for (var i1 = 0; i1 < data.Count; i1++) {
		Nif.NifStream(out data[i1], s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numStrings = (uint)data.Count;
	Nif.NifStream(numStrings, s, info);
	for (var i1 = 0; i1 < data.Count; i1++) {
		Nif.NifStream(data[i1], s, info);
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
	numStrings = (uint)data.Count;
	s.AppendLine($"  Num Strings:  {numStrings}");
	array_output_count = 0;
	for (var i1 = 0; i1 < data.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Data[{i1}]:  {data[i1]}");
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

        //--BEGIN:FILE FOOT--//
        /*!
         * Gets or sets the string values stored in this object.
         * \param[in] n The new string values to store in this object.
         */
        public IList<string> Data
        {
            get => data; //: was cloning
            set => data = value; //: was cloning
        }
	//--END:CUSTOM--//
}

}
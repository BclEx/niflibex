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
 * Abstract base class for dynamic effects such as NiLights or projected texture
 * effects.
 */
public class NiDynamicEffect : NiAVObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiDynamicEffect", NiAVObject.TYPE);
	/*! If true, then the dynamic effect is applied to affected nodes during rendering. */
	internal bool switchState;
	/*!  */
	internal uint numAffectedNodes;
	/*!
	 * If a node appears in this list, then its entire subtree will be affected by the
	 * effect.
	 */
	internal IList<NiNode> affectedNodes;
	/*!
	 * As of 4.0 the pointer hash is no longer stored alongside each NiObject on disk,
	 * yet this node list still refers to the pointer hashes. Cannot leave the type as
	 * Ptr because the link will be invalid.
	 */
	internal IList<uint> affectedNodePointers;

	public NiDynamicEffect() {
	switchState = 1;
	numAffectedNodes = (uint)0;
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
public static NiObject Create() => new NiDynamicEffect();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	if ((info.version >= 0x0A01006A) && ((info.userVersion2 < 130))) {
		Nif.NifStream(out switchState, s, info);
	}
	if (info.version <= 0x04000002) {
		Nif.NifStream(out numAffectedNodes, s, info);
	}
	if (info.version <= 0x0303000D) {
		affectedNodes = new *[numAffectedNodes];
		for (var i2 = 0; i2 < affectedNodes.Count; i2++) {
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}
	}
	if ((info.version >= 0x04000000) && (info.version <= 0x04000002)) {
		affectedNodePointers = new uint[numAffectedNodes];
		for (var i2 = 0; i2 < affectedNodePointers.Count; i2++) {
			Nif.NifStream(out affectedNodePointers[i2], s, info);
		}
	}
	if ((info.version >= 0x0A010000) && ((info.userVersion2 < 130))) {
		Nif.NifStream(out (uint)numAffectedNodes, s, info);
		affectedNodes = new *[numAffectedNodes];
		for (var i2 = 0; i2 < affectedNodes.Count; i2++) {
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numAffectedNodes = (uint)affectedNodes.Count;
	if ((info.version >= 0x0A01006A) && ((info.userVersion2 < 130))) {
		Nif.NifStream(switchState, s, info);
	}
	if (info.version <= 0x04000002) {
		Nif.NifStream(numAffectedNodes, s, info);
	}
	if (info.version <= 0x0303000D) {
		for (var i2 = 0; i2 < affectedNodes.Count; i2++) {
			WriteRef((NiObject)affectedNodes[i2], s, info, link_map, missing_link_stack);
		}
	}
	if ((info.version >= 0x04000000) && (info.version <= 0x04000002)) {
		for (var i2 = 0; i2 < affectedNodePointers.Count; i2++) {
			Nif.NifStream(affectedNodePointers[i2], s, info);
		}
	}
	if ((info.version >= 0x0A010000) && ((info.userVersion2 < 130))) {
		Nif.NifStream((uint)numAffectedNodes, s, info);
		for (var i2 = 0; i2 < affectedNodes.Count; i2++) {
			WriteRef((NiObject)affectedNodes[i2], s, info, link_map, missing_link_stack);
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
	numAffectedNodes = (uint)affectedNodes.Count;
	s.AppendLine($"  Switch State:  {switchState}");
	s.AppendLine($"  Num Affected Nodes:  {numAffectedNodes}");
	array_output_count = 0;
	for (var i1 = 0; i1 < affectedNodes.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Affected Nodes[{i1}]:  {affectedNodes[i1]}");
		array_output_count++;
	}
	array_output_count = 0;
	for (var i1 = 0; i1 < affectedNodePointers.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Affected Node Pointers[{i1}]:  {affectedNodePointers[i1]}");
		array_output_count++;
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	if (info.version <= 0x0303000D) {
		for (var i2 = 0; i2 < affectedNodes.Count; i2++) {
			affectedNodes[i2] = FixLink<NiNode>(objects, link_stack, missing_link_stack, info);
		}
	}
	if ((info.version >= 0x0A010000) && ((info.userVersion2 < 130))) {
		for (var i2 = 0; i2 < affectedNodes.Count; i2++) {
			affectedNodes[i2] = FixLink<NiNode>(objects, link_stack, missing_link_stack, info);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < affectedNodes.Count; i1++) {
	}
	for (var i1 = 0; i1 < affectedNodes.Count; i1++) {
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < affectedNodes.Count; i1++) {
		if (affectedNodes[i1] != null)
			ptrs.Add((NiObject)affectedNodes[i1]);
	}
	for (var i1 = 0; i1 < affectedNodes.Count; i1++) {
	}
	return ptrs;
}

//--BEGIN:FILE FOOT--//
        /*!
         * Gets or sets the current switch state for this effect.  Perhaps this turns effect on and off?
         * \param[in] value The new switch state for this object.
         */
        public bool SwitchState
        {
            get => switchState;
            set => switchState = value;
        }

        /*!
         * Gets or sets what appears to be a list of the nodes that will be affected by this effect.
         * \param[in] value A list of the new nodes that will be affected by this effect.
         */
        public IList<NiNode> AffectedNodes
        {
            get => affectedNodes;
            set => affectedNodes = value;
        }
//--END:CUSTOM--//

}

}
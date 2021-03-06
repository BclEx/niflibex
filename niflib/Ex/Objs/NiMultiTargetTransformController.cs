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

/*! DEPRECATED (20.6) */
public class NiMultiTargetTransformController : NiInterpController {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiMultiTargetTransformController", NiInterpController.TYPE);
	/*! The number of target pointers that follow. */
	internal ushort numExtraTargets;
	/*! NiNode Targets to be controlled. */
	internal IList<NiAVObject> extraTargets;

	public NiMultiTargetTransformController() {
	numExtraTargets = (ushort)0;
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
public static NiObject Create() => new NiMultiTargetTransformController();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out numExtraTargets, s, info);
	extraTargets = new *[numExtraTargets];
	for (var i1 = 0; i1 < extraTargets.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numExtraTargets = (ushort)extraTargets.Count;
	Nif.NifStream(numExtraTargets, s, info);
	for (var i1 = 0; i1 < extraTargets.Count; i1++) {
		WriteRef((NiObject)extraTargets[i1], s, info, link_map, missing_link_stack);
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
	numExtraTargets = (ushort)extraTargets.Count;
	s.AppendLine($"  Num Extra Targets:  {numExtraTargets}");
	array_output_count = 0;
	for (var i1 = 0; i1 < extraTargets.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Extra Targets[{i1}]:  {extraTargets[i1]}");
		array_output_count++;
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < extraTargets.Count; i1++) {
		extraTargets[i1] = FixLink<NiAVObject>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < extraTargets.Count; i1++) {
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < extraTargets.Count; i1++) {
		if (extraTargets[i1] != null)
			ptrs.Add((NiObject)extraTargets[i1]);
	}
	return ptrs;
}

//--BEGIN:FILE FOOT--//
        /*!
         * Retrieves a list of the extra NiAVObject targets to be controlled.
         * \return The NiAVObject targets to be controlled.
         */
        public IList<NiAVObject> GetExtraTargets()
        {
            var retval = new List<NiAVObject>();
            var n = extraTargets.Count;
            retval.Reserve(n);
            for (var i = 0; i < n; ++i)
                retval.Add(new NiAVObject(extraTargets[i]));
            return retval;
        }

        /*!
         * Sets the list of the extra NiAVObject targets to be controlled.
         * \param[in] value The new NiAVObject targets to be controlled.
         */
        public void SetExtraTargets(IList<NiAVObject> value)
        {
            extraTargets.clear();
            size_t n = value.size();
            extraTargets.reserve(n);
            for (size_t i = 0; i < n; ++i)
                extraTargets.push_back(value[i]);
            extraTargets.erase(std::remove(extraTargets.begin(), extraTargets.end(), (NiAVObject*)NULL), extraTargets.end());
        }

        /*!
         * Adds a single extra target to the collection. The collection will expand if necessary.
         * \param[in] mesh The avobject to add to the collection.
         */
        public bool AddExtraTarget(NiAVObject target)
        {
            vector<NiAVObject*> & targets = extraTargets;
            vector<NiAVObject*>::iterator itr = std::find(targets.begin(), targets.end(), target);
            if (itr == targets.end())
            {
                targets.push_back(target);
                numExtraTargets++;

                return true;
            }

            return false;
        }

        /*!
         * Remove a single extra target from the collection.
         * \param[in] mesh The avobject remove from the collection.
         */
        public bool RemoveExtraTarget(NiAVObject target)
        {
            vector<NiAVObject*> & targets = extraTargets;
            vector<NiAVObject*>::iterator itr = std::find(targets.begin(), targets.end(), target);
            if (itr == targets.end())
            {
                targets.erase(itr);
                numExtraTargets--;

                return true;
            }

            return false;
        }
        /*!
         * Replace a single extra target by another in the specified extra target group.
         * \param[in] newmesh The avobject put into the collection.
         * \param[in] oldmesh The avobject remove from collection.
         */
        public bool ReplaceExtraTarget(NiAVObject newtarget, NiAVObject oldtarget)
        {
            vector<NiAVObject*> & targets = extraTargets;
            vector<NiAVObject*>::iterator itr = std::find(targets.begin(), targets.end(), oldtarget);
            if (itr != targets.end())
            {
                *itr = newtarget;

                return true;
            }

            return false;
        }
//--END:CUSTOM--//

}

}
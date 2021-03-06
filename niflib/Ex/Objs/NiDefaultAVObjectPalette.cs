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

/*! NiAVObjectPalette implementation. Used to quickly look up objects by name. */
public class NiDefaultAVObjectPalette : NiAVObjectPalette {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiDefaultAVObjectPalette", NiAVObjectPalette.TYPE);
	/*! Scene root of the object palette. */
	internal NiAVObject scene;
	/*! Number of objects. */
	internal uint numObjs;
	/*! The objects. */
	internal IList<AVObject> objs;

	public NiDefaultAVObjectPalette() {
	scene = null;
	numObjs = (uint)0;
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
public static NiObject Create() => new NiDefaultAVObjectPalette();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out numObjs, s, info);
	objs = new AVObject[numObjs];
	for (var i1 = 0; i1 < objs.Count; i1++) {
		Nif.NifStream(out objs[i1].name, s, info);
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numObjs = (uint)objs.Count;
	WriteRef((NiObject)scene, s, info, link_map, missing_link_stack);
	Nif.NifStream(numObjs, s, info);
	for (var i1 = 0; i1 < objs.Count; i1++) {
		Nif.NifStream(objs[i1].name, s, info);
		WriteRef((NiObject)objs[i1].avObject, s, info, link_map, missing_link_stack);
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
	numObjs = (uint)objs.Count;
	s.AppendLine($"  Scene:  {scene}");
	s.AppendLine($"  Num Objs:  {numObjs}");
	array_output_count = 0;
	for (var i1 = 0; i1 < objs.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Name:  {objs[i1].name}");
		s.AppendLine($"    AV Object:  {objs[i1].avObject}");
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	scene = FixLink<NiAVObject>(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < objs.Count; i1++) {
		objs[i1].avObject = FixLink<NiAVObject>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < objs.Count; i1++) {
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	if (scene != null)
		ptrs.Add((NiObject)scene);
	for (var i1 = 0; i1 < objs.Count; i1++) {
		if (objs[i1].avObject != null)
			ptrs.Add((NiObject)objs[i1].avObject);
	}
	return ptrs;
}

//--BEGIN:FILE FOOT--//

        /*!
         * Gets the AV Objects stored in this palette.
         * \return The AV Objects stored in this palette.
         */
        public IList<NiAVObject> GetObjs()
        {
            var objRefs = new List<NiAVObject>();
            foreach (var itr in objs)
                objRefs.Add(itr.avObject);
            return objRefs;
        }

        /*!
         * Sets the AV Objects stored in this palette.
         * \return The new AV Objects to be stored in this palette.
         */
        public void SetObjs(IList<NiAVObject> value)
        {
            objs.Clear();
            foreach (var itr in value)
            {
                var obj = new AVObject();
                obj.name = itr.Name;
                obj.avObject = itr;
                objs.Add(obj);
            }
        }

        /*!
         * Adds a single object to the collection. The collection will expand if necessary.
         * \param[in] mesh The object to add to the collection.
         */
        public bool AddObj(NiAVObject obj)
        {
            foreach (var itr in objs)
                if (itr.avObject == obj)
                    return false;
            var avo = new AVObject();
            avo.name = obj.Name;
            avo.avObject = obj;
            objs.Add(avo);
            numObjs++;
            return false;
        }

        /*!
         * Remove a single object from the collection.
         * \param[in] mesh The object remove from the collection.
         */
        public bool RemoveObj(NiAVObject obj)
        {
            foreach (var itr in objs.ToList())
                if (itr.avObject == obj)
                {
                    objs.Remove(itr);
                    numObjs--;
                    return true;
                }
            return false;
        }

        /*!
         * Replace a single object by another in the collection.
         * \param[in] newobj The object put into the collection.
         * \param[in] oldobj The object remove from the collection.
         */
        public void ReplaceObj(NiAVObject newobj, NiAVObject oldobj)
        {
            foreach (var itr in objs)
                if (itr.avObject == oldobj)
                {
                    itr.name = newobj.Name;
                    itr.avObject = newobj;
                }
        }
//--END:CUSTOM--//

}

}
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

/*! LEGACY (pre-10.1) particle modifier. */
public class NiParticleMeshModifier : NiParticleModifier {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiParticleMeshModifier", NiParticleModifier.TYPE);
	/*!  */
	internal uint numParticleMeshes;
	/*!  */
	internal IList<NiAVObject> particleMeshes;

	public NiParticleMeshModifier() {
	numParticleMeshes = (uint)0;
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
public static NiObject Create() => new NiParticleMeshModifier();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out numParticleMeshes, s, info);
	particleMeshes = new Ref[numParticleMeshes];
	for (var i1 = 0; i1 < particleMeshes.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numParticleMeshes = (uint)particleMeshes.Count;
	Nif.NifStream(numParticleMeshes, s, info);
	for (var i1 = 0; i1 < particleMeshes.Count; i1++) {
		WriteRef((NiObject)particleMeshes[i1], s, info, link_map, missing_link_stack);
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
	numParticleMeshes = (uint)particleMeshes.Count;
	s.AppendLine($"  Num Particle Meshes:  {numParticleMeshes}");
	array_output_count = 0;
	for (var i1 = 0; i1 < particleMeshes.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Particle Meshes[{i1}]:  {particleMeshes[i1]}");
		array_output_count++;
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < particleMeshes.Count; i1++) {
		particleMeshes[i1] = FixLink<NiAVObject>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < particleMeshes.Count; i1++) {
		if (particleMeshes[i1] != null)
			refs.Add((NiObject)particleMeshes[i1]);
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < particleMeshes.Count; i1++) {
	}
	return ptrs;
}


}

}
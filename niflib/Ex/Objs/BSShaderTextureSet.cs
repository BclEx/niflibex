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

/*! Bethesda-specific Texture Set. */
public class BSShaderTextureSet : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("BSShaderTextureSet", NiObject.TYPE);
	/*!  */
	internal int numTextures;
	/*!
	 * Textures.
	 *             0: Diffuse
	 *             1: Normal/Gloss
	 *             2: Glow(SLSF2_Glow_Map)/Skin/Hair/Rim light(SLSF2_Rim_Lighting)
	 *             3: Height/Parallax
	 *             4: Environment
	 *             5: Environment Mask
	 *             6: Subsurface for Multilayer Parallax
	 *             7: Back Lighting Map (SLSF2_Back_Lighting)
	 */
	internal IList<string> textures;

	public BSShaderTextureSet() {
	numTextures = (int)6;
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
public static NiObject Create() => new BSShaderTextureSet();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out numTextures, s, info);
	textures = new string[numTextures];
	for (var i1 = 0; i1 < textures.Count; i1++) {
		Nif.NifStream(out textures[i1], s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numTextures = (int)textures.Count;
	Nif.NifStream(numTextures, s, info);
	for (var i1 = 0; i1 < textures.Count; i1++) {
		Nif.NifStream(textures[i1], s, info);
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
	numTextures = (int)textures.Count;
	s.AppendLine($"  Num Textures:  {numTextures}");
	array_output_count = 0;
	for (var i1 = 0; i1 < textures.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Textures[{i1}]:  {textures[i1]}");
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
        // Textures
        // \param[in] value The new value.
        public IList<string> Textures
        {
            get => textures;
            set => textures = value;
        }

        // Textures
        // \return The current value.
        public string GetTexture(int i)
        {
            if (i >= textures.Count)
                throw new Exception("Invalid Texture Index specified");
            return textures[i];
        }
    
        // Textures
        // \param[in] i Index of texture to set
        // \param[in] value The new value.
        public void SetTexture(int i, string value)
        {
            if (i >= textures.Count)
                textures.Resize(i + 1);
            textures[i] = value;
        }
//--END:CUSTOM--//

}

}
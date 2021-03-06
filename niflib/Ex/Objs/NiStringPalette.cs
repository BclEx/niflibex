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


namespace Niflib
{

    /*!
     * List of 0x00-seperated strings, which are names of controlled objects and
     * controller types. Used in .kf files in conjunction with NiControllerSequence.
     */
    public class NiStringPalette : NiObject
    {
        //Definition of TYPE constant
        public static readonly Type_ TYPE = new Type_("NiStringPalette", NiObject.TYPE);
        /*! A bunch of 0x00 seperated strings. */
        internal StringPalette palette;

        public NiStringPalette()
        {
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
        public static NiObject Create() => new NiStringPalette();

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
        {

            base.Read(s, link_stack, info);
            Nif.NifStream(out palette.palette, s, info);
            Nif.NifStream(out palette.length, s, info);

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.Write(s, link_map, missing_link_stack, info);
            Nif.NifStream(palette.palette, s, info);
            Nif.NifStream(palette.length, s, info);

        }

        /*!
         * Summarizes the information contained in this object in English.
         * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
         * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
         */
        public override string AsString(bool verbose = false)
        {

            var s = new System.Text.StringBuilder();
            uint array_output_count = 0;
            s.Append(base.AsString());
            s.AppendLine($"    Palette:  {palette.palette}");
            s.AppendLine($"    Length:  {palette.length}");
            return s.ToString();

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.FixLinks(objects, link_stack, missing_link_stack, info);

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetRefs()
        {
            var refs = base.GetRefs();
            return refs;
        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetPtrs()
        {
            var ptrs = base.GetPtrs();
            return ptrs;
        }
        //--BEGIN:FILE FOOT--//
        /*!
         * Gets or sets the contents of the entire palette string.  This is a buffer of characters that will contain the all the strings stored in this palette.  It is usually better to add individual strings with the NiStringPalette::AddSubStr function.
         * \param[in] n The new palette string.  This will overwrite all existing strings stored in the palette.
         */
        public string PaletteString
        {
            get => palette.palette;
            set => palette.palette = value;
        }

        /*!
         * Retrieves a particular sub string from the palette by offset into the string palette.
         * \param[in] offset The offset into the string palette where the desired sub string starts.  I.e. the number of characters that preceed it in the string palette.
         * \return The sub string starting at the specified offset in the string palette.
         */
        public string GetSubStr(short offset)
        {
            throw new NotImplementedException();
            //  var r = string.Empty;
            //  // -1 is a null offset
            //  if (offset == -1)
            //      return r;
            //  for (var i = offset; i < palette.palette.Length; ++i)
            //      if (palette.palette[i] == '\0')
            //          break;
            //r.Add(palette.palette[i]);
            //  }
            //  return r;
        }

        /*!
         * Adds a new sub string to the end of the string palete and returns the offset position where it was added.
         * \param[in] n The new sub string to add.
         * \return The offset into the string palette where the new sub string was added.  I.e. the number of characters that preceed it in the string palette.
         */
        public uint AddSubStr(string value)
        {
            throw new NotImplementedException();
            ////Search for the string
            ////  When searching for strings also search for ending null.
            //var offset = (uint)palette.palette.find(n.c_str(), 0, n.size() + 1);
            ////If string was not found, append it
            //if (offset == 0xFFFFFFFF)
            //{
            //    offset = (unsigned int)palette.palette.size();
            //    palette.palette.append(value + '\0');
            //}
            ////Return the offset where the string was found or appended
            //return offset;
        }
        //--END:CUSTOM--//

    }

}
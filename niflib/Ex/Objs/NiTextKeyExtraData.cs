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

    /*! Extra data, used to name different animation sequences. */
    public class NiTextKeyExtraData : NiExtraData
    {
        //Definition of TYPE constant
        public static readonly Type_ TYPE = new Type_("NiTextKeyExtraData", NiExtraData.TYPE);
        /*! Unknown.  Always equals zero in all official files. */
        internal uint unknownInt1;
        /*! The number of text keys that follow. */
        internal uint numTextKeys;
        /*!
         * List of textual notes and at which time they take effect. Used for designating
         * the start and stop of animations and the triggering of sounds.
         */
        internal IList<Key<IndexString>> textKeys;

        public NiTextKeyExtraData()
        {
            unknownInt1 = (uint)0;
            numTextKeys = (uint)0;
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
        public static NiObject Create() => new NiTextKeyExtraData();

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
        {

            base.Read(s, link_stack, info);
            if (info.version <= 0x04020200)
            {
                Nif.NifStream(out unknownInt1, s, info);
            }
            Nif.NifStream(out numTextKeys, s, info);
            textKeys = new Key[numTextKeys];
            for (var i1 = 0; i1 < textKeys.Count; i1++)
            {
                Nif.NifStream(out textKeys[i1], s, info, 1);
            }

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.Write(s, link_map, missing_link_stack, info);
            numTextKeys = (uint)textKeys.Count;
            if (info.version <= 0x04020200)
            {
                Nif.NifStream(unknownInt1, s, info);
            }
            Nif.NifStream(numTextKeys, s, info);
            for (var i1 = 0; i1 < textKeys.Count; i1++)
            {
                Nif.NifStream(textKeys[i1], s, info, 1);
            }

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
            numTextKeys = (uint)textKeys.Count;
            s.AppendLine($"  Unknown Int 1:  {unknownInt1}");
            s.AppendLine($"  Num Text Keys:  {numTextKeys}");
            array_output_count = 0;
            for (var i1 = 0; i1 < textKeys.Count; i1++)
            {
                if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                {
                    s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
                    break;
                }
                if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                {
                    break;
                }
                s.AppendLine($"    Text Keys[{i1}]:  {textKeys[i1]}");
                array_output_count++;
            }
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
        /*! Sets the text note key data.
         * \param keys A vector containing new Key<string> data which will replace any existing data.
         * \sa NiKeyframeData::GetKeys, Key
         */
        public IList<Key<string>> Keys
        {
            get
            {
                var value = new List<Key<string>>();
                foreach (var itr in textKeys)
                {
                    var key = new Key<string>();
                    key.time = itr.time;
                    key.data = itr.data;
                    key.tension = itr.tension;
                    key.bias = itr.bias;
                    key.continuity = itr.continuity;
                    value.Add(key);
                }
                return value;
            }
            set
            {
                textKeys.Clear();
                foreach (var itr in value)
                {
                    var key = new Key<IndexString>();
                    key.time = itr.time;
                    key.data = itr.data;
                    key.tension = itr.tension;
                    key.bias = itr.bias;
                    key.continuity = itr.continuity;
                    textKeys.Add(key);
                }
            }
            //TODO:  There is an unknown member in this class
            //--END:CUSTOM--//
        }

    }
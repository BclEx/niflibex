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
 * LEGACY (pre-10.1)
 *         Unknown
 */
public class NiBezierMesh : NiAVObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiBezierMesh", NiAVObject.TYPE);
	/*! references. */
	public uint numBezierTriangles;
	/*! unknown */
	public NiBezierTriangle4[] bezierTriangle;
	/*! Unknown. */
	public uint unknown3;
	/*! Data count. */
	public ushort count1;
	/*! Unknown. */
	public ushort unknown4;
	/*! data. */
	public Vector3[] points1;
	/*! Unknown (illegal link?). */
	public uint unknown5;
	/*! data. */
	public Array2<float>[] points2;
	/*! unknown */
	public uint unknown6;
	/*! data count 2. */
	public ushort count2;
	/*! data count. */
	public Array4<ushort>[] data2;

	public NiBezierMesh() {
	numBezierTriangles = (uint)0;
	unknown3 = (uint)0;
	count1 = (ushort)0;
	unknown4 = (ushort)0;
	unknown5 = (uint)0;
	unknown6 = (uint)0;
	count2 = (ushort)0;
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
public static NiObject Create() => new NiBezierMesh();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out numBezierTriangles, s, info);
	bezierTriangle = new Ref[numBezierTriangles];
	for (var i1 = 0; i1 < bezierTriangle.Length; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out unknown3, s, info);
	Nif.NifStream(out count1, s, info);
	Nif.NifStream(out unknown4, s, info);
	points1 = new Vector3[count1];
	for (var i1 = 0; i1 < points1.Length; i1++) {
		Nif.NifStream(out points1[i1], s, info);
	}
	Nif.NifStream(out unknown5, s, info);
	points2 = new float[count1];
	for (var i1 = 0; i1 < points2.Length; i1++) {
		for (var i2 = 0; i2 < 2; i2++) {
			Nif.NifStream(out points2[i1][i2], s, info);
		}
	}
	Nif.NifStream(out unknown6, s, info);
	Nif.NifStream(out count2, s, info);
	data2 = new ushort[count2];
	for (var i1 = 0; i1 < data2.Length; i1++) {
		for (var i2 = 0; i2 < 4; i2++) {
			Nif.NifStream(out data2[i1][i2], s, info);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	count2 = (ushort)data2.Length;
	count1 = (ushort)points1.Length;
	numBezierTriangles = (uint)bezierTriangle.Length;
	Nif.NifStream(numBezierTriangles, s, info);
	for (var i1 = 0; i1 < bezierTriangle.Length; i1++) {
		WriteRef((NiObject)bezierTriangle[i1], s, info, link_map, missing_link_stack);
	}
	Nif.NifStream(unknown3, s, info);
	Nif.NifStream(count1, s, info);
	Nif.NifStream(unknown4, s, info);
	for (var i1 = 0; i1 < points1.Length; i1++) {
		Nif.NifStream(points1[i1], s, info);
	}
	Nif.NifStream(unknown5, s, info);
	for (var i1 = 0; i1 < points2.Length; i1++) {
		for (var i2 = 0; i2 < 2; i2++) {
			Nif.NifStream(points2[i1][i2], s, info);
		}
	}
	Nif.NifStream(unknown6, s, info);
	Nif.NifStream(count2, s, info);
	for (var i1 = 0; i1 < data2.Length; i1++) {
		for (var i2 = 0; i2 < 4; i2++) {
			Nif.NifStream(data2[i1][i2], s, info);
		}
	}

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string asString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	uint array_output_count = 0;
	s.Append(base.asString());
	count2 = (ushort)data2.Length;
	count1 = (ushort)points1.Length;
	numBezierTriangles = (uint)bezierTriangle.Length;
	s.AppendLine($"  Num Bezier Triangles:  {numBezierTriangles}");
	array_output_count = 0;
	for (var i1 = 0; i1 < bezierTriangle.Length; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Bezier Triangle[{i1}]:  {bezierTriangle[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Unknown 3:  {unknown3}");
	s.AppendLine($"  Count 1:  {count1}");
	s.AppendLine($"  Unknown 4:  {unknown4}");
	array_output_count = 0;
	for (var i1 = 0; i1 < points1.Length; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Points 1[{i1}]:  {points1[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Unknown 5:  {unknown5}");
	array_output_count = 0;
	for (var i1 = 0; i1 < points2.Length; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		for (var i2 = 0; i2 < 2; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Points 2[{i2}]:  {points2[i1][i2]}");
			array_output_count++;
		}
	}
	s.AppendLine($"  Unknown 6:  {unknown6}");
	s.AppendLine($"  Count 2:  {count2}");
	array_output_count = 0;
	for (var i1 = 0; i1 < data2.Length; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		for (var i2 = 0; i2 < 4; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Data 2[{i2}]:  {data2[i1][i2]}");
			array_output_count++;
		}
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < bezierTriangle.Length; i1++) {
		bezierTriangle[i1] = FixLink<NiBezierTriangle4>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < bezierTriangle.Length; i1++) {
		if (bezierTriangle[i1] != null)
			refs.Add((NiObject)bezierTriangle[i1]);
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < bezierTriangle.Length; i1++) {
	}
	return ptrs;
}


}

}
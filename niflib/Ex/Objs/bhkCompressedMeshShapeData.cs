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

/*! A compressed mesh shape for collision in Skyrim. */
public class bhkCompressedMeshShapeData : bhkRefObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkCompressedMeshShapeData", bhkRefObject.TYPE);
	/*! Number of bits in the shape-key reserved for a triangle index */
	internal uint bitsPerIndex;
	/*! Number of bits in the shape-key reserved for a triangle index and its winding */
	internal uint bitsPerWIndex;
	/*!
	 * Mask used to get the triangle index and winding from a shape-key (common: 262143
	 * = 0x3ffff)
	 */
	internal uint maskWIndex;
	/*! Mask used to get the triangle index from a shape-key (common: 131071 = 0x1ffff) */
	internal uint maskIndex;
	/*! The radius of the storage mesh shape? Quantization error? */
	internal float error;
	/*!
	 * The minimum boundary of the AABB (the coordinates of the corner with the lowest
	 * numerical values)
	 */
	internal Vector4 boundsMin;
	/*!
	 * The maximum boundary of the AABB (the coordinates of the corner with the highest
	 * numerical values)
	 */
	internal Vector4 boundsMax;
	/*!  */
	internal byte weldingType;
	/*!  */
	internal byte materialType;
	/*!  */
	internal uint numMaterials32;
	/*! Does not appear to be used. */
	internal IList<uint> materials32;
	/*!  */
	internal uint numMaterials16;
	/*! Does not appear to be used. */
	internal IList<uint> materials16;
	/*!  */
	internal uint numMaterials8;
	/*! Does not appear to be used. */
	internal IList<uint> materials8;
	/*! Number of chunk materials */
	internal uint numMaterials;
	/*! Table (array) with sets of materials. Chunks refers to this table by index. */
	internal IList<bhkCMSDMaterial> chunkMaterials;
	/*!  */
	internal uint numNamedMaterials;
	/*! Number of chunk transformations */
	internal uint numTransforms;
	/*!
	 * Table (array) with sets of transformations. Chunks refers to this table by
	 * index.
	 */
	internal IList<bhkCMSDTransform> chunkTransforms;
	/*!  */
	internal uint numBigVerts;
	/*! Compressed Vertices? */
	internal IList<Vector4> bigVerts;
	/*!  */
	internal uint numBigTris;
	/*!  */
	internal IList<bhkCMSDBigTris> bigTris;
	/*!  */
	internal uint numChunks;
	/*!  */
	internal IList<bhkCMSDChunk> chunks;
	/*! Does not appear to be used. Needs array. */
	internal uint numConvexPieceA;

	public bhkCompressedMeshShapeData() {
	bitsPerIndex = (uint)0;
	bitsPerWIndex = (uint)0;
	maskWIndex = (uint)0;
	maskIndex = (uint)0;
	error = 0.0f;
	weldingType = (byte)0;
	materialType = (byte)0;
	numMaterials32 = (uint)0;
	numMaterials16 = (uint)0;
	numMaterials8 = (uint)0;
	numMaterials = (uint)0;
	numNamedMaterials = (uint)0;
	numTransforms = (uint)0;
	numBigVerts = (uint)0;
	numBigTris = (uint)0;
	numChunks = (uint)0;
	numConvexPieceA = (uint)0;
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
public static NiObject Create() => new bhkCompressedMeshShapeData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out bitsPerIndex, s, info);
	Nif.NifStream(out bitsPerWIndex, s, info);
	Nif.NifStream(out maskWIndex, s, info);
	Nif.NifStream(out maskIndex, s, info);
	Nif.NifStream(out error, s, info);
	Nif.NifStream(out boundsMin, s, info);
	Nif.NifStream(out boundsMax, s, info);
	Nif.NifStream(out weldingType, s, info);
	Nif.NifStream(out materialType, s, info);
	Nif.NifStream(out numMaterials32, s, info);
	materials32 = new uint[numMaterials32];
	for (var i1 = 0; i1 < materials32.Count; i1++) {
		Nif.NifStream(out materials32[i1], s, info);
	}
	Nif.NifStream(out numMaterials16, s, info);
	materials16 = new uint[numMaterials16];
	for (var i1 = 0; i1 < materials16.Count; i1++) {
		Nif.NifStream(out materials16[i1], s, info);
	}
	Nif.NifStream(out numMaterials8, s, info);
	materials8 = new uint[numMaterials8];
	for (var i1 = 0; i1 < materials8.Count; i1++) {
		Nif.NifStream(out materials8[i1], s, info);
	}
	Nif.NifStream(out numMaterials, s, info);
	chunkMaterials = new bhkCMSDMaterial[numMaterials];
	for (var i1 = 0; i1 < chunkMaterials.Count; i1++) {
		Nif.NifStream(out chunkMaterials[i1].material, s, info);
		if ((info.version <= 0x14000005) && ((info.userVersion2 < 16))) {
			Nif.NifStream(out chunkMaterials[i1].filter.layer_ob, s, info);
		}
		if (((info.version == 0x14020007) && (info.userVersion2 <= 34))) {
			Nif.NifStream(out chunkMaterials[i1].filter.layer_fo, s, info);
		}
		if (((info.version == 0x14020007) && (info.userVersion2 > 34))) {
			Nif.NifStream(out chunkMaterials[i1].filter.layer_sk, s, info);
		}
		Nif.NifStream(out chunkMaterials[i1].filter.flagsAndPartNumber, s, info);
		Nif.NifStream(out chunkMaterials[i1].filter.group, s, info);
	}
	Nif.NifStream(out numNamedMaterials, s, info);
	Nif.NifStream(out numTransforms, s, info);
	chunkTransforms = new bhkCMSDTransform[numTransforms];
	for (var i1 = 0; i1 < chunkTransforms.Count; i1++) {
		Nif.NifStream(out chunkTransforms[i1].translation, s, info);
		Nif.NifStream(out chunkTransforms[i1].rotation.x, s, info);
		Nif.NifStream(out chunkTransforms[i1].rotation.y, s, info);
		Nif.NifStream(out chunkTransforms[i1].rotation.z, s, info);
		Nif.NifStream(out chunkTransforms[i1].rotation.w, s, info);
	}
	Nif.NifStream(out numBigVerts, s, info);
	bigVerts = new Vector4[numBigVerts];
	for (var i1 = 0; i1 < bigVerts.Count; i1++) {
		Nif.NifStream(out bigVerts[i1], s, info);
	}
	Nif.NifStream(out numBigTris, s, info);
	bigTris = new bhkCMSDBigTris[numBigTris];
	for (var i1 = 0; i1 < bigTris.Count; i1++) {
		Nif.NifStream(out bigTris[i1].triangle1, s, info);
		Nif.NifStream(out bigTris[i1].triangle2, s, info);
		Nif.NifStream(out bigTris[i1].triangle3, s, info);
		Nif.NifStream(out bigTris[i1].material, s, info);
		Nif.NifStream(out bigTris[i1].weldingInfo, s, info);
	}
	Nif.NifStream(out numChunks, s, info);
	chunks = new bhkCMSDChunk[numChunks];
	for (var i1 = 0; i1 < chunks.Count; i1++) {
		Nif.NifStream(out chunks[i1].translation, s, info);
		Nif.NifStream(out chunks[i1].materialIndex, s, info);
		Nif.NifStream(out chunks[i1].reference, s, info);
		Nif.NifStream(out chunks[i1].transformIndex, s, info);
		Nif.NifStream(out chunks[i1].numVertices, s, info);
		chunks[i1].vertices = new ushort[chunks[i1].numVertices];
		for (var i2 = 0; i2 < chunks[i1].vertices.Count; i2++) {
			Nif.NifStream(out chunks[i1].vertices[i2], s, info);
		}
		Nif.NifStream(out chunks[i1].numIndices, s, info);
		chunks[i1].indices = new ushort[chunks[i1].numIndices];
		for (var i2 = 0; i2 < chunks[i1].indices.Count; i2++) {
			Nif.NifStream(out chunks[i1].indices[i2], s, info);
		}
		Nif.NifStream(out chunks[i1].numStrips, s, info);
		chunks[i1].strips = new ushort[chunks[i1].numStrips];
		for (var i2 = 0; i2 < chunks[i1].strips.Count; i2++) {
			Nif.NifStream(out chunks[i1].strips[i2], s, info);
		}
		Nif.NifStream(out chunks[i1].numWeldingInfo, s, info);
		chunks[i1].weldingInfo = new ushort[chunks[i1].numWeldingInfo];
		for (var i2 = 0; i2 < chunks[i1].weldingInfo.Count; i2++) {
			Nif.NifStream(out chunks[i1].weldingInfo[i2], s, info);
		}
	}
	Nif.NifStream(out numConvexPieceA, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numChunks = (uint)chunks.Count;
	numBigTris = (uint)bigTris.Count;
	numBigVerts = (uint)bigVerts.Count;
	numTransforms = (uint)chunkTransforms.Count;
	numMaterials = (uint)chunkMaterials.Count;
	numMaterials8 = (uint)materials8.Count;
	numMaterials16 = (uint)materials16.Count;
	numMaterials32 = (uint)materials32.Count;
	Nif.NifStream(bitsPerIndex, s, info);
	Nif.NifStream(bitsPerWIndex, s, info);
	Nif.NifStream(maskWIndex, s, info);
	Nif.NifStream(maskIndex, s, info);
	Nif.NifStream(error, s, info);
	Nif.NifStream(boundsMin, s, info);
	Nif.NifStream(boundsMax, s, info);
	Nif.NifStream(weldingType, s, info);
	Nif.NifStream(materialType, s, info);
	Nif.NifStream(numMaterials32, s, info);
	for (var i1 = 0; i1 < materials32.Count; i1++) {
		Nif.NifStream(materials32[i1], s, info);
	}
	Nif.NifStream(numMaterials16, s, info);
	for (var i1 = 0; i1 < materials16.Count; i1++) {
		Nif.NifStream(materials16[i1], s, info);
	}
	Nif.NifStream(numMaterials8, s, info);
	for (var i1 = 0; i1 < materials8.Count; i1++) {
		Nif.NifStream(materials8[i1], s, info);
	}
	Nif.NifStream(numMaterials, s, info);
	for (var i1 = 0; i1 < chunkMaterials.Count; i1++) {
		Nif.NifStream(chunkMaterials[i1].material, s, info);
		if ((info.version <= 0x14000005) && ((info.userVersion2 < 16))) {
			Nif.NifStream(chunkMaterials[i1].filter.layer_ob, s, info);
		}
		if (((info.version == 0x14020007) && (info.userVersion2 <= 34))) {
			Nif.NifStream(chunkMaterials[i1].filter.layer_fo, s, info);
		}
		if (((info.version == 0x14020007) && (info.userVersion2 > 34))) {
			Nif.NifStream(chunkMaterials[i1].filter.layer_sk, s, info);
		}
		Nif.NifStream(chunkMaterials[i1].filter.flagsAndPartNumber, s, info);
		Nif.NifStream(chunkMaterials[i1].filter.group, s, info);
	}
	Nif.NifStream(numNamedMaterials, s, info);
	Nif.NifStream(numTransforms, s, info);
	for (var i1 = 0; i1 < chunkTransforms.Count; i1++) {
		Nif.NifStream(chunkTransforms[i1].translation, s, info);
		Nif.NifStream(chunkTransforms[i1].rotation.x, s, info);
		Nif.NifStream(chunkTransforms[i1].rotation.y, s, info);
		Nif.NifStream(chunkTransforms[i1].rotation.z, s, info);
		Nif.NifStream(chunkTransforms[i1].rotation.w, s, info);
	}
	Nif.NifStream(numBigVerts, s, info);
	for (var i1 = 0; i1 < bigVerts.Count; i1++) {
		Nif.NifStream(bigVerts[i1], s, info);
	}
	Nif.NifStream(numBigTris, s, info);
	for (var i1 = 0; i1 < bigTris.Count; i1++) {
		Nif.NifStream(bigTris[i1].triangle1, s, info);
		Nif.NifStream(bigTris[i1].triangle2, s, info);
		Nif.NifStream(bigTris[i1].triangle3, s, info);
		Nif.NifStream(bigTris[i1].material, s, info);
		Nif.NifStream(bigTris[i1].weldingInfo, s, info);
	}
	Nif.NifStream(numChunks, s, info);
	for (var i1 = 0; i1 < chunks.Count; i1++) {
		chunks[i1].numWeldingInfo = (uint)chunks[i1].weldingInfo.Count;
		chunks[i1].numStrips = (uint)chunks[i1].strips.Count;
		chunks[i1].numIndices = (uint)chunks[i1].indices.Count;
		chunks[i1].numVertices = (uint)chunks[i1].vertices.Count;
		Nif.NifStream(chunks[i1].translation, s, info);
		Nif.NifStream(chunks[i1].materialIndex, s, info);
		Nif.NifStream(chunks[i1].reference, s, info);
		Nif.NifStream(chunks[i1].transformIndex, s, info);
		Nif.NifStream(chunks[i1].numVertices, s, info);
		for (var i2 = 0; i2 < chunks[i1].vertices.Count; i2++) {
			Nif.NifStream(chunks[i1].vertices[i2], s, info);
		}
		Nif.NifStream(chunks[i1].numIndices, s, info);
		for (var i2 = 0; i2 < chunks[i1].indices.Count; i2++) {
			Nif.NifStream(chunks[i1].indices[i2], s, info);
		}
		Nif.NifStream(chunks[i1].numStrips, s, info);
		for (var i2 = 0; i2 < chunks[i1].strips.Count; i2++) {
			Nif.NifStream(chunks[i1].strips[i2], s, info);
		}
		Nif.NifStream(chunks[i1].numWeldingInfo, s, info);
		for (var i2 = 0; i2 < chunks[i1].weldingInfo.Count; i2++) {
			Nif.NifStream(chunks[i1].weldingInfo[i2], s, info);
		}
	}
	Nif.NifStream(numConvexPieceA, s, info);

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
	numChunks = (uint)chunks.Count;
	numBigTris = (uint)bigTris.Count;
	numBigVerts = (uint)bigVerts.Count;
	numTransforms = (uint)chunkTransforms.Count;
	numMaterials = (uint)chunkMaterials.Count;
	numMaterials8 = (uint)materials8.Count;
	numMaterials16 = (uint)materials16.Count;
	numMaterials32 = (uint)materials32.Count;
	s.AppendLine($"  Bits Per Index:  {bitsPerIndex}");
	s.AppendLine($"  Bits Per W Index:  {bitsPerWIndex}");
	s.AppendLine($"  Mask W Index:  {maskWIndex}");
	s.AppendLine($"  Mask Index:  {maskIndex}");
	s.AppendLine($"  Error:  {error}");
	s.AppendLine($"  Bounds Min:  {boundsMin}");
	s.AppendLine($"  Bounds Max:  {boundsMax}");
	s.AppendLine($"  Welding Type:  {weldingType}");
	s.AppendLine($"  Material Type:  {materialType}");
	s.AppendLine($"  Num Materials 32:  {numMaterials32}");
	array_output_count = 0;
	for (var i1 = 0; i1 < materials32.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Materials 32[{i1}]:  {materials32[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Materials 16:  {numMaterials16}");
	array_output_count = 0;
	for (var i1 = 0; i1 < materials16.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Materials 16[{i1}]:  {materials16[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Materials 8:  {numMaterials8}");
	array_output_count = 0;
	for (var i1 = 0; i1 < materials8.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Materials 8[{i1}]:  {materials8[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Materials:  {numMaterials}");
	array_output_count = 0;
	for (var i1 = 0; i1 < chunkMaterials.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Material:  {chunkMaterials[i1].material}");
		s.AppendLine($"    Layer:  {chunkMaterials[i1].filter.layer_ob}");
		s.AppendLine($"    Layer:  {chunkMaterials[i1].filter.layer_fo}");
		s.AppendLine($"    Layer:  {chunkMaterials[i1].filter.layer_sk}");
		s.AppendLine($"    Flags and Part Number:  {chunkMaterials[i1].filter.flagsAndPartNumber}");
		s.AppendLine($"    Group:  {chunkMaterials[i1].filter.group}");
	}
	s.AppendLine($"  Num Named Materials:  {numNamedMaterials}");
	s.AppendLine($"  Num Transforms:  {numTransforms}");
	array_output_count = 0;
	for (var i1 = 0; i1 < chunkTransforms.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Translation:  {chunkTransforms[i1].translation}");
		s.AppendLine($"    x:  {chunkTransforms[i1].rotation.x}");
		s.AppendLine($"    y:  {chunkTransforms[i1].rotation.y}");
		s.AppendLine($"    z:  {chunkTransforms[i1].rotation.z}");
		s.AppendLine($"    w:  {chunkTransforms[i1].rotation.w}");
	}
	s.AppendLine($"  Num Big Verts:  {numBigVerts}");
	array_output_count = 0;
	for (var i1 = 0; i1 < bigVerts.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Big Verts[{i1}]:  {bigVerts[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Big Tris:  {numBigTris}");
	array_output_count = 0;
	for (var i1 = 0; i1 < bigTris.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Triangle 1:  {bigTris[i1].triangle1}");
		s.AppendLine($"    Triangle 2:  {bigTris[i1].triangle2}");
		s.AppendLine($"    Triangle 3:  {bigTris[i1].triangle3}");
		s.AppendLine($"    Material:  {bigTris[i1].material}");
		s.AppendLine($"    Welding Info:  {bigTris[i1].weldingInfo}");
	}
	s.AppendLine($"  Num Chunks:  {numChunks}");
	array_output_count = 0;
	for (var i1 = 0; i1 < chunks.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		chunks[i1].numWeldingInfo = (uint)chunks[i1].weldingInfo.Count;
		chunks[i1].numStrips = (uint)chunks[i1].strips.Count;
		chunks[i1].numIndices = (uint)chunks[i1].indices.Count;
		chunks[i1].numVertices = (uint)chunks[i1].vertices.Count;
		s.AppendLine($"    Translation:  {chunks[i1].translation}");
		s.AppendLine($"    Material Index:  {chunks[i1].materialIndex}");
		s.AppendLine($"    Reference:  {chunks[i1].reference}");
		s.AppendLine($"    Transform Index:  {chunks[i1].transformIndex}");
		s.AppendLine($"    Num Vertices:  {chunks[i1].numVertices}");
		array_output_count = 0;
		for (var i2 = 0; i2 < chunks[i1].vertices.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Vertices[{i2}]:  {chunks[i1].vertices[i2]}");
			array_output_count++;
		}
		s.AppendLine($"    Num Indices:  {chunks[i1].numIndices}");
		array_output_count = 0;
		for (var i2 = 0; i2 < chunks[i1].indices.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Indices[{i2}]:  {chunks[i1].indices[i2]}");
			array_output_count++;
		}
		s.AppendLine($"    Num Strips:  {chunks[i1].numStrips}");
		array_output_count = 0;
		for (var i2 = 0; i2 < chunks[i1].strips.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Strips[{i2}]:  {chunks[i1].strips[i2]}");
			array_output_count++;
		}
		s.AppendLine($"    Num Welding Info:  {chunks[i1].numWeldingInfo}");
		array_output_count = 0;
		for (var i2 = 0; i2 < chunks[i1].weldingInfo.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Welding Info[{i2}]:  {chunks[i1].weldingInfo[i2]}");
			array_output_count++;
		}
	}
	s.AppendLine($"  Num Convex Piece A:  {numConvexPieceA}");
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
         * Gets or sets bitsPerIndex 
         * \param[in] value bitsPerIndex
         */
        public virtual uint BitsPerIndex
        {
            get => bitsPerIndex;
            set => bitsPerIndex = value;
        }

        /*!
         * Gets or sets bitsPerWIndex 
         * \param[in] value bitsPerWIndex
         */
        public virtual uint BitsPerWIndex
        {
            get => bitsPerWIndex;
            set => bitsPerWIndex = value;
        }

        /*!
         * Gets or sets maskWIndex 
         * \param[in] value maskWIndex
         */
        public virtual uint MaskWIndex
        {
            get => maskWIndex;
            set => maskWIndex = value;
        }

        /*!
         * Gets or sets maskIndex
         * \param[in] value maskIndex
         */
        public virtual uint MaskIndex
        {
            get => maskIndex;
            set => maskIndex = value;
        }

        /*!
         * Gets or sets error
         * \param[in] value error
         */
        public virtual float Error
        {
            get => error;
            set => error = value;
        }

        /*!
         * Gets or sets boundsMin
         * \param[in] value boundsMin
         */
        public virtual Vector4 BoundsMin
        {
            get => boundsMin;
            set => boundsMin = value;
        }

        /*!
         * Gets or sets boundsMax
         * \param[in] value boundsMax
         */
        public virtual Vector4 BoundsMax
        {
            get => boundsMax;
            set => boundsMax = value;
        }

        /*!
         * Gets or sets numMaterials
         * \param[in] value numMaterials
         */
        public virtual uint NumMaterials
        {
            get => numMaterials;
            set => numMaterials = value;
        }

        /*!
         * Gets or sets list of chunkMaterials 
         * \param[in] List of the chunkMaterials
         */
        public virtual IList<bhkCMSDMaterial> ChunkMaterials
        {
            get => chunkMaterials;
            set
            {
                numMaterials = (uint)value.Count;
                chunkMaterials = value;
            }
        }

        /*!
         * Gets or sets numTransforms
         * \param[in] value numTransforms
         */
        public virtual uint NumTransforms
        {
            get => numTransforms;
            set => numTransforms = value;
        }

        /*!
         * Gets or set list of chunkTransforms 
         * \param[in] List of the chunkTransforms
         */
        public virtual IList<bhkCMSDTransform> ChunkTransforms
        {
            get => chunkTransforms;
            set
            {
                numTransforms = (uint)value.Count;
                chunkTransforms = value;
            }
        }

        /*!
         * Gets or sets numBigVerts
         * \param[in] value numBigVerts
         */
        public virtual uint NumBigVerts
        {
            get => numBigVerts;
            set => numBigVerts = value;
        }

        /*!
         * Gets or sets list of bigVerts 
         * \param[in] List of the bigVerts
         */
        public virtual IList<Vector4> BigVerts
        {
            get => bigVerts;
            set
            {
                numBigVerts = (uint)value.Count;
                bigVerts = value;
            }
        }

        /*!
         * Gets or sets numBigTris
         * \param[in] value numBigTris
         */
        public virtual uint NumBigTris
        {
            get => numBigTris;
            set => numBigTris = value;
        }

        /*!
         * Gets or set list of bigTris 
         * \param[in] List of the bigTris
         */
        public virtual IList<bhkCMSDBigTris> BigTris
        {
            get => bigTris;
            set
            {
                numBigTris = (uint)value.Count;
                bigTris = value;
            }
        }

        /*!
         * Gets or sets numChunks
         * \param[in] value numChunks
         */
        public virtual uint NumChunks
        {
            get => numChunks;
            set => numChunks = value;
        }

        /*!
         * Gets or sets list of chunks 
         * \param[in] List of the chunks
         */
        public virtual IList<bhkCMSDChunk> Chunks
        {
            get => chunks;
            set
            {
                numChunks = (uint)value.Count;
                chunks = value;
            }
        }
//--END:CUSTOM--//

}

}
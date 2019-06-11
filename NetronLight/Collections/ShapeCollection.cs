using System;
using System.Collections;
namespace NetronLight
{
	/// <summary>
	/// STC of shapes
	/// </summary>
	public class ShapeCollection: CollectionBase
	{
		public ShapeCollection(){}

		public int Add(ShapeBase shape)
		{
			return this.InnerList.Add(shape);
		}

		public ShapeBase this[int index]
		{
			get{return this.InnerList[index] as ShapeBase;}
		}
		public void Remove(ShapeBase shape)
		{
			this.InnerList.Remove(shape);
		}
	}


}

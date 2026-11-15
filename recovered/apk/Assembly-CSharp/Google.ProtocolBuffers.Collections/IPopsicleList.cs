using System.Collections;
using System.Collections.Generic;

namespace Google.ProtocolBuffers.Collections;

public interface IPopsicleList<T> : IEnumerable, IList<T>, ICollection<T>, IEnumerable<T>
{
	void Add(IEnumerable<T> collection);
}

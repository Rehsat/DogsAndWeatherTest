using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IParser<TReturnType>
{
    public TReturnType Parse(string json);
}

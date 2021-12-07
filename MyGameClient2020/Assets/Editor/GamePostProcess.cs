//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.Build;
//using UnityEditor.Rendering;
//using UnityEngine;

//public class GamePostProcess //: IPreprocessShaders
//{
//    public int callbackOrder => throw new System.NotImplementedException();

//    public void OnProcessShader(Shader shader, ShaderSnippetData snippet, IList<ShaderCompilerData> data)
//    {
//        //throw new System.NotImplementedException();
//    }

//    //public void OnProcessShader(Shader shader, ShaderSnippetData snippet, IList<ShaderCompilerData> data)
//    //{
//    //    // 跳过处理系统shader，不处理
//    //    // return;

//    //    // 读取对应shader的变体集:
//    //    // 上一步我们为每一个使用到的shader都创建了独立的编译集合
//    //    // 获取指定shader的编译信息

//    //    var comb = ShaderUtils.ParseShaderCombinations(shader, true);
//    //    // 跳过一些完全Use其他shader，自己不含有代码的shader， 不处理
//    //    // return;

//    //    // 反向遍历，利于删除操作
//    //    for (int i = data.Count - 1; i >= 0; --i)
//    //    {
//    //        // 当前编译单元中的变体关键字列表
//    //        var _keywords = data[i].shaderKeywordSet.GetShaderKeywords();
//    //        // 只剔除有关键字的情形，减少代码复杂度
//    //        // 实际上，无关键字的变体也可能被丢弃不用，简单舍弃这次剔除操作并不会增加太多编译负担
//    //        if (_keywords.Length > 0)
//    //        {
//    //            var keywordList = new HashSet<String>();
//    //            for (int j = 0; j < _keywords.Length; ++j)
//    //            {
//    //                var name = _keywords[j].GetKeywordName();
//    //                fullKeywords.Add(name);
//    //                if (snippetCombinations.multi_compiles != null)
//    //                {
//    //                    if (Array.IndexOf(snippetCombinations.multi_compiles, name) < 0)
//    //                    {
//    //                        // 排除multi_compiles编译宏，这些是必须使用的，不能剔除
//    //                        // 这里只添加不含multi_compile关键字
//    //                        keywordList.Add(name);
//    //                    }
//    //                }
//    //            }
//    //            if (keywordList.Count > 0)
//    //            {
//    //                // 说明这个变体的关键字时可以被剔除编译的
//    //                // 进一步判定：
//    //                // 由这一关键字序列构成的变体，是否在我们提前存储的变体集资源中出现

//    //                // 在遍历判定已经使用的变体集的时候，注意要把含有multi_compile项
//    //                // 的关键字去掉，在无序对比，如果能完全匹配，则说明当前次编译的
//    //                // shader变体可能会使用，否则就剔除
//    //                // ...

//    //                var matched = false;
//    //                // 遍历所有从项目中搜集到的变体
//    //                for (int n = 0; n < rawVariants.Count; ++n)
//    //                {
//    //                    var variant = rawVariants[n];
//    //                    var matchCount = -1;
//    //                    var mismatchCount = 0;
//    //                    var skipCount = 0;
//    //                    if (variant.shader == shader && variant.passType == snippet.passType)
//    //                    {
//    //                        matchCount = 0;

//    //                        // 需要说明一下：
//    //                        // 查找匹配的变体时，需要排除multi_compiles关键字
//    //                        // snippetCombinations数据从手工解析ParsedCombinations-XXX.shader而来
//    //                        // 如果直接调用ShaderUtil.GetShaderVariantEntries，可能会因为全变体数量过大而内存爆掉

//    //                        for (var m = 0; m < variant.keywords.Length; ++m)
//    //                        {
//    //                            var keyword = variant.keywords[m];
//    //                            if (Array.IndexOf(snippetCombinations.multi_compiles, keyword) < 0)
//    //                            {
//    //                                if (keywordList.Contains(keyword))
//    //                                {
//    //                                    ++matchCount;
//    //                                }
//    //                                else
//    //                                {
//    //                                    ++mismatchCount;
//    //                                    break;
//    //                                }
//    //                            }
//    //                            else
//    //                            {
//    //                                ++skipCount;
//    //                            }
//    //                        }
//    //                    }
//    //                    if (matchCount >= 0 && mismatchCount == 0 && matchCount + skipCount == keywordList.Count)
//    //                    {
//    //                        matched = true;
//    //                        break;
//    //                    }
//    //                }
//    //                if (!matched)
//    //                {
//    //                    data.RemoveAt(i);
//    //                }
//    //            }
//    //        }
//    //    }
//    //}

//}

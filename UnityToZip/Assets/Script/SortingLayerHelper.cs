using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GameCore;
namespace GameLogic {
    //public class SortingLayerHelper
    //{
    //    static Dictionary<string, Q_effectExpandBean> effectConfigs;
    //    static Dictionary<string, DirLayers> skillLayers = new Dictionary<string, DirLayers>();
    //    /// <summary>
    //    /// 获取特效层级
    //    /// </summary>
    //    /// <param name="name"></param>
    //    /// <param name="dir"></param>
    //    /// <returns></returns>
    //    public static string GetEffectSortingLayer(string name)
    //    {
    //        if (effectConfigs == null) {
    //            effectConfigs = new Dictionary<string, Q_effectExpandBean>();
    //            List<Q_effectExpandBean> list = DataManager.Ins.GetEffectContainer.GetList();
    //            for (int i = 0; i < list.Count; i++) {
    //                effectConfigs.Add(list[i].q_name, list[i]);
    //            }
    //        }
    //        Q_effectExpandBean bean = null;
    //        if (effectConfigs.TryGetValue(name, out bean)) {
    //            return bean.q_defaultLayer;
    //        }
    //        //BaseLogger.LogFormat("这个特效没有配置渲染层级:{0}", name);
    //        return "Default";
    //    }

    //    /// <summary>
    //    /// 获取技能特效层级
    //    /// </summary>
    //    /// <param name="name"></param>
    //    /// <param name="dir"></param>
    //    /// <returns></returns>
    //    public static string GetSkillEffectSortingLayer(string name,int dir)
    //    {
    //        if (effectConfigs == null)
    //        {
    //            effectConfigs = new Dictionary<string, Q_effectExpandBean>();
    //            List<Q_effectExpandBean> list = DataManager.Ins.GetEffectContainer.GetList();
    //            for (int i = 0; i < list.Count; i++)
    //            {
    //                effectConfigs.Add(list[i].q_name, list[i]);
    //            }
    //        }
    //        Q_effectExpandBean bean = null;
    //        if (effectConfigs.TryGetValue(name, out bean))
    //        {
    //            DirLayers layers = null;
    //            if (!string.IsNullOrEmpty(bean.q_ext))
    //            {
    //                if (!skillLayers.TryGetValue(name, out layers))
    //                {
    //                    layers = new DirLayers(bean.q_ext);
    //                    skillLayers.Add(name, layers);
    //                }
    //                DirLayer layer = layers.GetLayer(dir);
    //                if (layer != null) return layer.SortingLayer;
    //            }
    //            return bean.q_defaultLayer;
    //        }
    //        //BaseLogger.LogFormat("这个特效没有配置渲染层级:{0}",name);
    //        return "Default";
    //    }
    //}

    //public class DirLayers
    //{
    //    public List<DirLayer> layers;

    //    public DirLayers(string json) {
    //        if (string.IsNullOrEmpty(json))
    //            layers = new List<DirLayer>();
    //        else
    //            layers = LitJson.JsonMapper.ToObject<List<DirLayer>>(json);
    //    }

    //    public DirLayer GetLayer(int dir) {
    //        for (int i = 0; i < layers.Count; i++) {
    //            if (dir == layers[i].dir) return layers[i];
    //        }
    //        return null;
    //    }
    //}

    public class DirLayer
    {
        public int dir;
        public string SortingLayer;
    }
}

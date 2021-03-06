/*
 * SDK Pullenti Lingvo, version 4.5, april 2021. Copyright (c) 2013, Pullenti. All rights reserved. 
 * Non-Commercial Freeware and Commercial Software.
 * This class is generated using the converter Unisharping (www.unisharping.ru) from Pullenti C# project. 
 * The latest version of the code is available on the site www.pullenti.ru
 */

using System;

namespace Pullenti.Ner.Goods
{
    /// <summary>
    /// Типы атрибута
    /// </summary>
    public enum GoodAttrType : int
    {
        /// <summary>
        /// Неопределено
        /// </summary>
        Undefined,
        /// <summary>
        /// Ключевое слово (тип товара)
        /// </summary>
        Keyword,
        /// <summary>
        /// Качественное свойство
        /// </summary>
        Character,
        /// <summary>
        /// Собственное имя
        /// </summary>
        Proper,
        /// <summary>
        /// Модель
        /// </summary>
        Model,
        /// <summary>
        /// Количественное свойство
        /// </summary>
        Numeric,
        /// <summary>
        /// Ссылка на некоторую сущность (страна, организация - производитель, ГОСТ ...)
        /// </summary>
        Referent,
    }
}
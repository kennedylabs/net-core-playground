//using KennedyLabsWebsite.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using static System.Math;

//namespace AccountsWebsite.Infrastructure.DataAccess
//{
//    public interface IHierarchical : IIdentifiable
//    {
//        int Ordinal { get; set; }
//    }

//    public interface IHierarchicalWithSections : IHierarchical
//    {
//        ICollection<SectionModel> Sections { get; set; }
//    }

//    public interface IHierarchicalWithItems : IHierarchical
//    {
//        ICollection<ItemModel> Items { get; set; }
//    }

//    public static class Hierarchical
//    {
//        public static IEnumerable<SectionModel> Fixup(this IHierarchical entity,
//            IHierarchical newEntity, PageModel page)
//        {

//        }

//        public static async Task FixupSections(
//            this IHierarchicalWithSections entity, IHierarchicalWithSections oldEntity,
//            PageModel page, IRepository<SectionModel> repo = null)
//        {
//            await FixupChildren(entity, oldEntity, e => e.Sections, (e1, e2) =>
//                e1.Title == e2.Title && e1.SecondaryText == e2.SecondaryText &&
//                e1.TertiaryText == e2.TertiaryText, repo);
//        }

//        public static async Task FixupItems(
//            this IHierarchicalWithItems entity, IHierarchicalWithItems oldEntity,
//            IRepository<ItemModel> repo = null)
//        {
//            await FixupChildren(entity, oldEntity, e => e.Items, (e1, e2) =>
//                e1.Text == e2.Text && e1.Context == e2.Context && e1.Type == e2.Type, repo);
//        }

//        private static async Task FixupChildren<TEntity, TChild>(
//            TEntity entity, TEntity oldEntity, Func<TEntity, ICollection<TChild>> childrenSelector,
//            Func<TChild, TChild, bool> childrenEquater, PageModel page, IRepository<TChild> repo,
//            Tuple<IRepository<SectionModel>, IRepository<ItemModel>> repos)
//            where TEntity : IHierarchical where TChild : class, IHierarchical
//        {
//            var children = entity != null ? childrenSelector(entity) : null;
//            var oldChildren = oldEntity != null ? childrenSelector(oldEntity) : null;

//            for (int i = 0; i < Max(children?.Count ?? 0, oldChildren?.Count ?? 0); i++)
//            {
//                var child = children != null ? children.ElementAtOrDefault(i) : null;
//                var oldChild = oldChildren != null ? oldChildren.ElementAtOrDefault(i) : null;

//                if (child != null)
//                {
//                    child.Ordinal = i;

//                    if (oldChild == null)
//                    {
//                        await repo.CreateAsync(child, persist: false);
//                    }
//                    else if (!childrenEquater(child, oldChild))
//                    {
//                        child.Id = oldChild.Id;
//                        await repo.UpdateAsync(
//                            child.Id, child, persist: false, ignoreNotFound: true);
//                    }

//                    child.Fixup(oldChild, );
//                }
//                else if (repo != null && oldChild != null)
//                {
//                    await repo.DeleteAsync(oldChild, ignoreNotFound: true);
//                }
//            }
//        }
//    }
//}

using Business;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using PostEntity = DataAccess.Data.Post;

namespace API.Controllers.Post
{
    [Route("[controller]")]
    /// Controlador para gestionar las operaciones CRUD de PostEntity.
    public class PostController : ControllerBase
    {
        private BaseService<PostEntity> PostService;
        private PostService _postService1;

        /// <summary>
        /// Constructor que inicializa el servicio base con la entidad PostEntity.
        /// </summary>
        /// <param name="postService">El servicio base para la entidad PostEntity.</param>
        public PostController(BaseService<PostEntity> postService, PostService postService1)
        {
            PostService = postService;
            _postService1 = postService1;
        }

        /// <summary>
        /// Obtiene todas las entidades PostEntity.
        /// </summary>
        /// <returns>Una consulta IQueryable de todas las entidades PostEntity.</returns>
        [HttpGet()]
        public IQueryable<PostEntity> GetAll()
        {
            return PostService.GetAll();
        }

        /// <summary>
        /// Crea varias entidades PostEntity en una sola petición.
        /// </summary>
        /// <param name="entities">La lista de entidades PostEntity a crear.</param>
        /// <returns>Una lista de las entidades PostEntity creadas.</returns>
        [HttpPost()]
        public List<PostEntity> Create([FromBody] List<PostEntity> entities)
        {
            List<PostEntity> createdEntities = new List<PostEntity>();
            foreach (var entity in entities)
            {
                createdEntities.Add(_postService1.Create(entity));
            }
            return createdEntities;
        }

        /// <summary>
        /// Actualiza una entidad PostEntity existente.
        /// </summary>
        /// <param name="entity">La entidad PostEntity a actualizar.</param>
        /// <returns>La entidad PostEntity actualizada.</returns>
        [HttpPut()]
        public PostEntity Update([FromBodyAttribute] PostEntity entity)
        {
            return PostService.Update(entity.PostId, entity, out bool changed);
        }

        /// <summary>
        /// Elimina una entidad PostEntity existente.
        /// </summary>
        /// <param name="entity">La entidad PostEntity a eliminar.</param>
        /// <returns>La entidad PostEntity eliminada.</returns>
        [HttpDelete()]
        public PostEntity Delete([FromBodyAttribute] PostEntity entity)
        {
            return PostService.Create(entity);
        }
    }

}

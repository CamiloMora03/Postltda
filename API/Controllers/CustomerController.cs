using Business;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using CustomerEntity = DataAccess.Data.Customer;

namespace API.Controllers.Customer
{
    [Route("[controller]")]
    /// <summary>
    /// Controlador para gestionar las operaciones CRUD de CustomerEntity.
    /// </summary>
    public class CustomerController : ControllerBase
    {
        private BaseService<CustomerEntity> CustomerService1;
        private CustomerService _customerService;
        private readonly PostService _postService;

        /// <summary>
        /// Constructor que inicializa los servicios para la entidad CustomerEntity.
        /// </summary>
        /// <param name="customerService1">El servicio base para la entidad CustomerEntity.</param>
        /// <param name="customerService">El servicio específico para la entidad CustomerEntity.</param>
        public CustomerController(BaseService<CustomerEntity> customerService1, CustomerService customerService, PostService  postService)
        {
            CustomerService1 = customerService1;
            _customerService = customerService;
            _postService = postService;
        }

        /// <summary>
        /// Obtiene todas las entidades CustomerEntity.
        /// </summary>
        /// <returns>Una consulta IQueryable de todas las entidades CustomerEntity.</returns>
        [HttpGet()]
        public IQueryable<CustomerEntity> GetAll()
        {
            return CustomerService1.GetAll();
        }

        /// <summary>
        /// Crea una entidad CustomerEntity.
        /// </summary>
        /// <param name="entity">La entidad CustomerEntity a crear.</param>
        /// <returns>La entidad CustomerEntity creada.</returns>
        [HttpPost()]
        public CustomerEntity Create([FromBodyAttribute] CustomerEntity entity)
        {
            return CreateCustomer(entity);
        }

        /// <summary>
        /// Método privado para crear una entidad CustomerEntity.
        /// Comprueba si la entidad es nula y si ya existe un cliente con el mismo nombre.
        /// </summary>
        /// <param name="entity">La entidad CustomerEntity a crear.</param>
        /// <returns>La entidad CustomerEntity creada.</returns>
        private CustomerEntity CreateCustomer(CustomerEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var existingCustomer = _customerService.GetByName(entity.Name);

            if (existingCustomer != null)
            {
                throw new Exception("Ya existe un cliente con el mismo nombre.");
            }
            return _customerService.Create(entity);
        }

        /// <summary>
        /// Actualiza una entidad CustomerEntity existente.
        /// </summary>
        /// <param name="entity">La entidad CustomerEntity a actualizar.</param>
        /// <returns>La entidad CustomerEntity actualizada.</returns>
        [HttpPut()]
        public CustomerEntity Update(CustomerEntity entity)
        {
            return CustomerService1.Update(entity.CustomerId, entity, out bool changed);
        }

        /// <summary>
        /// Elimina una entidad CustomerEntity existente.
        /// </summary>
        /// <param name="entity">La entidad CustomerEntity a eliminar.</param>
        /// <returns>La entidad CustomerEntity eliminada.</returns>
        [HttpDelete()]
        public CustomerEntity Delete([FromBodyAttribute] CustomerEntity entity)
        {
            _postService.DeletePostsByCustomerId(entity.CustomerId);

            // Luego de eliminar los posts, se puede proceder a eliminar al cliente
            return CustomerService1.Delete(entity);
            
        }
    }

}

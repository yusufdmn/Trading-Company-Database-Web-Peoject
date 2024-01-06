using Web.Entities;
using Web.Repositories;

namespace Web.Handlers;

public class OrderCreationHandler
{

    private readonly OrderlineRepository _orderLineRepository;
    private readonly OrderRepository _orderRepository;


    public OrderCreationHandler(OrderRepository orderRepository, OrderlineRepository orderlineRepository)
    {
        _orderRepository = orderRepository;
        _orderLineRepository = orderlineRepository;
    }


    public async Task Handle(List<Orderline> orderLines,int customerId,string platformURL)
    {

        /*
         * Bana orderLines lar gelicek
         * orderLine in icindeki id degerleri -99 gelecek
         * 
         * bir order olusturacagim ve id sini alacagim.
         * 
         * daha sonra order linelari insert ederken bu parametreyi yollayacagim
         */
        int? orderId =null;
        try
        {
            orderId = await _orderRepository.Insert(null,customerId,platformURL);
            
            if(orderId == null)
            {
                throw new NullReferenceException(nameof(orderId));
            }

        }catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }


        foreach(var each in  orderLines)
        {
            try
            {
                await _orderLineRepository.Insert(each.Price,each.SKU,orderId);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }


    }



}

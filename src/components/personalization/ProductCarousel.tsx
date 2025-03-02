
import {
  Carousel,
  CarouselContent,
  CarouselItem,
  CarouselNext,
  CarouselPrevious,
} from "@/components/ui/carousel";
import { Button } from "@/components/ui/button";
import { ProductCategory } from "./types";
import { cn } from "@/lib/utils";
import { products } from "@/config/products";

interface ProductCarouselProps {
  categories: ProductCategory[];
  selectedCategory: string | null;
  onCategorySelect: (categoryId: string) => void;
}

const ProductCarousel = ({
  categories,
  selectedCategory,
  onCategorySelect,
}: ProductCarouselProps) => {
  return (
    <div className="w-full space-y-6 py-6">
      <div className="relative">
        <Carousel
          opts={{
            align: "start",
            loop: true,
          }}
          className="w-full"
        >
          <CarouselContent className="-ml-2 md:-ml-4">
            {products.map((product) => (
              <CarouselItem
                key={product.id}
                className="pl-2 md:pl-4 basis-1/2 md:basis-1/2 lg:basis-1/3"
              >
                <div
                  onClick={() => onCategorySelect(product.id)}
                  className={cn(
                    "group relative overflow-hidden rounded-xl bg-white shadow-lg transition-all duration-300 hover:shadow-xl cursor-pointer h-[500px] flex flex-col",
                    selectedCategory === product.id && "ring-2 ring-primary"
                  )}
                >
                  <div className="h-[300px] overflow-hidden bg-gray-50 flex items-center justify-center">
                    <img
                      src={product.image}
                      alt={product.name}
                      className="w-full h-full object-contain p-4"
                    />
                  </div>

                  <div className="flex flex-col flex-grow p-6 bg-white justify-between">
                    <div>
                      <h3 className="text-xl font-semibold mb-2 text-gray-800">{product.name}</h3>
                      <p className="text-sm text-gray-600 mb-4 line-clamp-3">
                        {product.description}
                      </p>
                    </div>
                    <div>
                      <p className="text-lg font-medium mb-4 text-primary">
                        À partir de {product.startingPrice} TND
                      </p>
                      <Button
                        className="w-full bg-primary hover:bg-primary/90 text-white transition-colors"
                        onClick={(e) => {
                          e.stopPropagation();
                          onCategorySelect(product.id);
                        }}
                      >
                        Personnaliser
                      </Button>
                    </div>
                  </div>
                </div>
              </CarouselItem>
            ))}
          </CarouselContent>
          <CarouselPrevious />
          <CarouselNext />
        </Carousel>
      </div>
    </div>
  );
};

export default ProductCarousel;

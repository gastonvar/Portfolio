import React, { useEffect, useState } from 'react'
import "./Consejos.css"

const Consejos = () => {
    const [consejos, setConsejos] = useState([])
    const [consejoActual, setConsejoActual] = useState(0)
    const [animacion, setAnimacion] = useState('fade-in')

    useEffect(() => {
        const consejosArray = [
            'Amamanta a tu bebé cada 2-3 horas.',
            'Cambia el pañal de tu bebé cada 2-4 horas.',
            'Dedica tiempo a jugar con tu bebé para estimular su desarrollo.',
            'Introduce alimentos sólidos a partir de los 6 meses.',
            'Baña a tu bebé 2-3 veces por semana.',
            'Utiliza juguetes de colores brillantes para captar su atención.',
            'Lleva a tu bebé al pediatra regularmente para chequeos de salud.',
            'Lee cuentos a tu bebé para estimular su desarrollo lingüístico.',
            'Asegúrate de que tu bebé duerma en una posición segura, preferiblemente boca arriba.',
            'Mantén a tu bebé hidratado con suficiente agua.',
            'Establece una rutina de sueño consistente para tu bebé.',
            'Masajea a tu bebé suavemente para relajarlo.',
            'Evita exponer a tu bebé al sol directo durante largos periodos.',
            'Canta canciones de cuna para calmar a tu bebé antes de dormir.',
            'Introduce nuevos alimentos de manera gradual para identificar posibles alergias.',
            'Juega a juegos de apilamiento para desarrollar la coordinación mano-ojo de tu bebé.',
            'Asegúrate de que tu bebé esté vacunado según el calendario recomendado.',
            'Haz ejercicios suaves con tu bebé para fortalecer sus músculos.',
            'Usa protector solar adecuado para bebés cuando los lleves al aire libre.',
            'Incorpora frutas y verduras en la dieta de tu bebé.',
            'Asegúrate de que tu bebé tenga tiempo para jugar libremente en el suelo.',
            'Desinfecta regularmente los juguetes y objetos que tu bebé utiliza.',
            'Introduce a tu bebé a diferentes texturas para estimular su sentido del tacto.',
            'Habla con tu bebé frecuentemente para fomentar su desarrollo del lenguaje.',
            'Evita el uso excesivo de dispositivos electrónicos cerca de tu bebé.',
            'Permite que tu bebé explore diferentes sonidos y música.',
            'Establece límites seguros para cuando tu bebé empiece a gatear.',
            'Proporciona a tu bebé ropa cómoda y adecuada para la temperatura.',
            'Cambia la posición de tu bebé durante el día para evitar la plagiocefalia (cabeza plana).',
            'Usa un humidificador en la habitación de tu bebé para mantener el aire húmedo.',
            'Evita el uso de mantas y almohadas en la cuna para reducir el riesgo de asfixia.',
            'Lava las manos antes de alimentar a tu bebé o tocar sus utensilios.',
            'Fomenta la independencia de tu bebé permitiéndole jugar solo bajo supervisión.',
            'Proporciona a tu bebé juguetes que promuevan la coordinación motora.',
            'Crea un ambiente tranquilo y oscuro para que tu bebé duerma mejor.',
            'Involucra a tu bebé en actividades sensoriales como tocar arena o agua.',
            'Habla de manera positiva y alentadora a tu bebé para construir su autoestima.',
            'Asegúrate de que tu bebé esté bien abrigado en climas fríos.',
            'Evita dar alimentos azucarados o procesados a tu bebé.',
            'Haz que tu bebé participe en actividades familiares para que se sienta incluido.',
            'Permite que tu bebé interactúe con otros niños para desarrollar sus habilidades sociales.',
            'Mantén un ambiente libre de humo para la salud de tu bebé.',
            'Practica el contacto piel a piel para fortalecer el vínculo con tu bebé.',
            'Mantén a tu bebé alejado de superficies duras o puntiagudas.',
            'Incorpora productos de higiene sin fragancia para la piel sensible de tu bebé.',
            'Usa detergentes suaves para lavar la ropa de tu bebé.',
            'Asegúrate de que tu bebé tenga suficiente tiempo de descanso durante el día.',
            'Coloca barreras de seguridad en escaleras y ventanas.',
            'Utiliza un asiento de seguridad adecuado en el coche para tu bebé.',
            'Lava y esteriliza los biberones después de cada uso.',
            'Consulta a un especialista si notas algún comportamiento inusual en tu bebé.'
        ];        
        
        setConsejos(consejosArray);

        const interval = setInterval(() => {
            setAnimacion('fade-out');
            setTimeout(() => {
                setConsejoActual((prev) => (prev + 1) % consejosArray.length);
                setAnimacion('fade-in');
            }, 1000); // Tiempo de fade-out antes de cambiar el consejo
        }, 7000); // Cambio de consejo cada 7 segundos

        return () => clearInterval(interval);
    }, [])

    return (
        <div className='consejos-container'>
            {consejos.length > 0 && (
                <div className={`consejo-item ${animacion}`}>
                    <p>{consejos[consejoActual]}</p>
                </div>
            )}
        </div>
    )
}

export default Consejos
